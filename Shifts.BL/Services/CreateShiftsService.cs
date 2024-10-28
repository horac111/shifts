using Microsoft.IdentityModel.Tokens;
using Shared.Extensions;
using Shifts.BL.Data.Other;
using Shifts.BL.Data.Records;
using Shifts.DAL.Abstracts.Repository;
using Shifts.DAL.Models;

namespace Shifts.BL.Services;
public class CreateShiftsService
{
    private readonly IOccurenceRepository occurenceRepository;
    private readonly IShiftDayRepository shiftDayRepository;
    private readonly IShiftRepository shiftRepository;
    private readonly IWorkingAvailabilityRepository workingAvailabilityRepository;

    public CreateShiftsService(IOccurenceRepository occurenceRepository, IShiftDayRepository shiftDayRepository, IShiftRepository shiftRepository, IWorkingAvailabilityRepository workingAvailabilityRepository)
    {
        this.occurenceRepository = occurenceRepository;
        this.shiftDayRepository = shiftDayRepository;
        this.shiftRepository = shiftRepository;
        this.workingAvailabilityRepository = workingAvailabilityRepository;
    }

    public async IAsyncEnumerable<ShiftCreationError> FillShiftsAsync(DateOnly month)
    {
        var shifts = await shiftRepository.GetAllValidAsync(month);
        Dictionary<ApplicationUser, UserUsage> usages = new();
        foreach (var shift in shifts)
        {
            var result = await FillShiftFirstAttemptAsync(shift, month);

            foreach (var error in result.Errors)
                yield return error;

            foreach (var usage in result.UserUsages)
            {
                if (usages.ContainsKey(usage.User))
                    usages[usage.User].WorkingDays.AddRange(usage.WorkingDays);
                else
                    usages.Add(usage.User, usage);
            }
        }
    }

    private async Task<(IEnumerable<ShiftCreationError> Errors, UserUsage[] UserUsages)> FillShiftFirstAttemptAsync(Shift shift, DateOnly month)
    {
        var to = Shared.Helpers.Math.Min(shift.End.GetValueOrMax(), month.AddMonths(1));

        List<ShiftCreationError> errors = new();
        var userUsage = (await workingAvailabilityRepository.GetAvailableUsersByShiftAndDurationAsync(shift, month, to))
            .Select(x => new UserUsage { User = x })
            .ToArray();

        var workingAvalabilities = await workingAvailabilityRepository.GetWorkingAvailabilitiesByShiftAndDurationAsync(shift, month, to);
        var occurences = await occurenceRepository.GetAllByShiftAndDuration(shift, month, to);
        var dates = await shiftDayRepository.GetAllByShiftAndDurationAsync(shift, month, to);

        foreach (var shiftDay in dates)
        {
            var toAssign = shift.MaxPeople + occurences.GetValueOrDefault(shiftDay.Date, Array.Empty<Occurence>())!.Sum(x => x.ChangeOfPeople);
            if (shiftDay.WorkingAvailability.IsNullOrEmpty())
            {
                errors.Add(new(shiftDay.Date, shift.Name, toAssign));
                continue;
            }

            int[] indexes = new int[toAssign];
            var assigned = AssignDay(workingAvalabilities[shiftDay.Date], shiftDay, userUsage, indexes, shift.Id, toAssign);

            if (assigned != toAssign)
                errors.Add(new(shiftDay.Date, shift.Name, toAssign - assigned));

            ResortArray(userUsage, indexes, assigned);
        }

        await shiftDayRepository.SaveAsync();

        return (errors, userUsage);
    }

    private long AssignDay(WorkingAvailability[] workingAvailabilities, ShiftDay day, UserUsage[] userUsages, int[] indexes, long shiftId, long toAssign)
    {
        long assigned = 0;
        var userOrder = userUsages.Select((x, i) => new { x, i })
            .ToDictionary(x => x.x.User, x => x.i);

        var orderedAvalabilities = workingAvailabilities
            .OrderByDescending(x => x.State)
            .ThenBy(x => userOrder[x.User]);

        foreach (var availability in orderedAvalabilities)
        {
            indexes[assigned] = userOrder[availability.User];
            assigned++;
            day.ShiftAssignments.Add(new() { User = availability.User, ShiftDay = day });
            userUsages[userOrder[availability.User]].WorkingDays.Add(new(shiftId, day.Date, availability.State));

            if (assigned == toAssign)
                break;
        }

        return assigned;
    }

    private void ResortArray(UserUsage[] arr, int[] changedIndexes, long count)
    {
        for (int i = 0; i < count; i++)
        {
            int changed = changedIndexes[i];
            UserUsage? item = arr[changed];

            for (int j = changed + 1; j < arr.Length; j++)
            {
                if (arr[j].Count > item.Count)
                    break;

                var temp = arr[j];
                arr[j] = item;
                arr[j - 1] = temp;
            }
        }
    }

    private void FillShiftsFinal(IEnumerable<Shift> shifts, DateOnly month, Dictionary<long, HashSet<DateOnly>> errors, UserUsage[] userUsages)
    {

    }
}

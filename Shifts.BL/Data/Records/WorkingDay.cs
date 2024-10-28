using Shared.Enums;

namespace Shifts.BL.Data.Records;
public record WorkingDay(long ShiftId, DateOnly Date, AvailabilityState State);

namespace Shifts.BL.Data.Records;
public record ShiftCreationError(DateOnly Date, string ShiftName, long UnasignableSlots);

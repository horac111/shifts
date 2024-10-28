namespace Shifts.BL.Abstract.Services;

public interface IShiftDaysService
{
    Task CreateMonth(DateOnly month);
}
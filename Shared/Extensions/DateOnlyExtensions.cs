namespace Shared.Extensions;
public static class DateOnlyExtensions
{
    public static DateOnly GetFirstInMonth(this DateOnly date) => new DateOnly(date.Year, date.Month, 1);
}

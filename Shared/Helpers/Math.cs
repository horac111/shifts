namespace Shared.Helpers;
public static class Math
{
    public static DateOnly Max(params DateOnly[] dates) => dates.Max();
    public static DateOnly Min(params DateOnly[] dates) => dates.Min();

    public static TimeOnly Max(params TimeOnly[] dates) => dates.Max();
    public static TimeOnly Min(params TimeOnly[] dates) => dates.Min();

    public static DateTime Max(params DateTime[] dates) => dates.Max();
    public static DateTime Min(params DateTime[] dates) => dates.Min();
}

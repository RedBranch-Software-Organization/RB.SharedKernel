namespace RB.SharedKernel.Extensions;

public static class DateTimeExtensions
{
    /// <summary>
    /// Check if a date is between two dates
    /// </summary>
    /// <param name="date"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    public static bool IsBetween(this DateTime date, DateTime startDate, DateTime endDate) 
        => startDate <= date
           && date <= endDate;
}

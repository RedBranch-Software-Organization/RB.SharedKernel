namespace RB.SharedKernel.Extensions;
public static class DateTimeExtensions
{
    public static bool IsBetween(this DateTime value, DateTime startDate, DateTime endDate) 
        => (startDate <= value && value <= endDate) 
        || (endDate <= value && value <= startDate);
}
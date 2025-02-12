using RB.SharedKernel.Extensions;
using FluentAssertions;
namespace RB.SharedKernel.UnitTests.Extensions;
public class DateTimeExtensionsTest
{
    [Theory]
    [InlineData("2025-01-01", "2025-01-01", "2025-01-01", true)]
    [InlineData("2025-01-01 00:00:00", "2025-01-01", "2025-01-01", true)]
    [InlineData("2025-01-01", "2025-01-01 00:00:00", "2025-01-01", true)]
    [InlineData("2025-01-01", "2025-01-01", "2025-01-01 00:00:00", true)]
    [InlineData("2025-01-01 00:00:00", "2025-01-01 00:00:00", "2025-01-01", true)]
    [InlineData("2025-01-01 00:00:00", "2025-01-01", "2025-01-01 00:00:00", true)]
    [InlineData("2025-01-01", "2025-01-01 00:00:00", "2025-01-01 00:00:00", true)]
    [InlineData("2025-01-01 00:00", "2025-01-01 00:00", "2025-01-01 00:00:00", true)]
    [InlineData("2025-01-01 00:00:00", "2025-01-01 00:00:00", "2025-01-01 00:00:00", true)]
    public void IsBetween_SameDates_ShouldReturnTrue(DateTime dateToCheck, DateTime startDate, DateTime endDate, bool expected)
    {
        // Act
        var result = dateToCheck.IsBetween(startDate, endDate);
        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("2025-01-02", "2025-01-02", "2025-01-01", true)]
    [InlineData("2025-01-01 09:00:00", "2025-01-03 21:00:00", "2025-01-01 00:00:00", true)]
    [InlineData("2025-01-01 00:00:01", "2025-01-01 23:59:59", "2025-01-01 00:00:00", true)]
    [InlineData("2025-01-01 00:00:02", "2025-01-01 00:00:03", "2025-01-01 00:00:01", true)]
    
    public void IsBetween_StartDateIsAfterEndDate_ShouldReturnTrue (DateTime dateToCheck, DateTime startDate, DateTime endDate, bool expected)
    {
        var result = dateToCheck.IsBetween(startDate, endDate);
        result.Should().Be(expected);
    }


    [Theory]
    [InlineData("2025-01-02", "2025-01-01", "2025-01-03", true)]
    [InlineData("2025-01-02 00:00:00", "2025-01-01 00:00:00", "2025-01-03 00:00:00", true)]
    [InlineData("2025-01-01 00:00:01", "2025-01-01 00:00:00", "2025-01-01 23:59:59", true)]
    [InlineData("2025-01-01 00:00:01", "2025-01-01 00:00:01", "2025-01-03T00:00:03", true)]

    public void IsBetween_StartDateIsBeforeEndDate_ShouldReturnTrue(DateTime dateToCheck, DateTime startDate, DateTime endDate, bool expected)
    {
        var result = dateToCheck.IsBetween(startDate, endDate);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("2025-01-01", "2025-01-02", "2025-01-02", false)]
    [InlineData("2025-01-02", "2025-01-01", "2025-01-01", false)]
    [InlineData("2025-01-01", "2025-01-02", "2025-01-03", false)]
    [InlineData("2025-01-04", "2025-01-02", "2025-01-03", false)]
    [InlineData("2025-01-01 00:00:00", "2025-01-02 00:00:00", "2025-01-02 00:00:00", false)]
    [InlineData("2025-01-02 00:00:00", "2025-01-01 00:00:00", "2025-01-01 00:00:00", false)]
    [InlineData("2025-01-01 00:00:00", "2025-01-02 00:00:00", "2025-01-03 00:00:00", false)]
    [InlineData("2025-01-04 00:00:00", "2025-01-02 00:00:00", "2025-01-03 00:00:00", false)]
    [InlineData("2025-01-01 00:00:00", "2025-01-01 10:00:00", "2025-01-01 20:00:00", false)]
    [InlineData("2025-01-02 23:59:59", "2025-01-01 10:00:00", "2025-01-02 20:00:00", false)]
    [InlineData("2025-01-01 09:59:59", "2025-01-01 10:00:00", "2025-01-01 20:00:00", false)]
    [InlineData("2025-01-02 20:00:01", "2025-01-01 10:00:00", "2025-01-02 20:00:00", false)]
    public void IsBetween_DateToCheckIsOutOfRange_ShouldReturnFalse(DateTime dateToCheck, DateTime startDate, DateTime endDate, bool expected)
    {
        var result = dateToCheck.IsBetween(startDate, endDate);
        result.Should().Be(expected);

    }
}

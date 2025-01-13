using RB.SharedKernel.Tests.Arrange;

namespace RB.SharedKernel.Tests;

public class EntityTests
{
    [Fact]
    public void EntityId_Visible()
    {
        Guid actual = Guid.NewGuid();
        var account = new Account(actual);
        var expected = account.Id;
        Assert.Equal(actual, expected);
    }
}

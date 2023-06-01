using SupplyPlanning.Domain.Tests.Tools;

namespace SupplyPlanning.Domain.Tests.TestUtils;

public abstract class TimeTravelingTest
{
    public StubClock Clock { get; set; }
    protected TimeTravelingTest()
    {
        this.Clock = new StubClock(DateTime.Now);
    }
}
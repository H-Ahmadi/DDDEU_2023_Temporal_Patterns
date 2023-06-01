using SupplyPlanning.Domain.Core;

namespace SupplyPlanning.Domain.Tests.Tools;

public class StubClock : IClock
{
    private DateTime _now;
    public StubClock(DateTime now)
    {
        _now = now;
    }
    public DateTime Now() => _now;
    public void TravelThroughTime(DateTime targetDate)
    {
        this._now = targetDate;
    }

    public void TravelThroughTime(string dateInString)
    {
        this._now = DateTime.Parse(dateInString);
    }
}
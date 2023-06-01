using System.Net.Http.Headers;
using SupplyPlanning.Domain.Core;

namespace SupplyPlanning.Domain.SupplyPlans;

public static class Schedule
{
    public static SupplyPlanBuilder SupplyPlan() => new SupplyPlanBuilder();
}

public class SupplyPlanBuilder
{
    private string _title;
    private DateRange _activePeriod;
    private List<DayOfWeek> _dayOfWeeks;
    private List<DemandItem> _demandItems = new();

    public SupplyPlanBuilder Named(string title)
    {
        this._title = title;
        return this;
    }
    public SupplyPlanBuilder BetweenDates(DateTime start, DateTime end)
    {
        this._activePeriod = new DateRange(start, end);
        return this;
    }
    public SupplyPlanBuilder OnDays(params DayOfWeek[] daysOfWeek)
    {
        _dayOfWeeks = daysOfWeek.ToList();
        return this;
    }

    public SupplyPlanBuilder IncludingDemand(DemandItem item)
    {
        this._demandItems.Add(item);
        return this;
    }
    public SupplyPlanBuilder IncludingDemands(List<DemandItem> items)
    {
        this._demandItems.AddRange(items);
        return this;
    }
    public SupplyPlan Build()
    {
        return new SupplyPlan(_title, _activePeriod, _dayOfWeeks, _demandItems);
    }
}


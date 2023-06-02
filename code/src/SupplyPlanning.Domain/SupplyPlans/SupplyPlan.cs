using System.Security.Principal;
using SupplyPlanning.Domain.Core;

namespace SupplyPlanning.Domain.SupplyPlans;

public class SupplyPlan
{
    public SupplyPlanId Id { get; private set; }
    public string Title { get; private set; }
    public DateRange ActivePeriod { get; private set; }
    public HistoricalData<SupplyPlanVersion> Versions { get; private set; }

    public SupplyPlan(string title, DateRange activePeriod, 
        List<DayOfWeek> scheduleDays, List<DemandItem> demandEntries, IClock clock)
    {
        Id = SupplyPlanId.New();
        Title = title;
        ActivePeriod = activePeriod;
        Versions = new HistoricalData<SupplyPlanVersion>();
        var firstVersion = new SupplyPlanVersion(scheduleDays, demandEntries);
        Versions.Set(firstVersion, clock.Now());
    }

    public SupplyPlan(string title, DateRange activePeriod, List<DayOfWeek> scheduleDays, List<DemandItem> demandEntries)
        : this(title, activePeriod, scheduleDays, demandEntries, SystemClock.Instance)
    {
    }

    public IEnumerable<DateTime> OccurrenceDays()
    {
        return ActivePeriod.Days()
            .Where(a => this.Versions.EffectiveValueAt(a).ScheduleDays.Contains(a.DayOfWeek))
            .ToList();
    }
    public List<DemandItem> TotalDemandsInRange(DateRange calculationRange)
    {
        //We're basically ignoring the unit of measure difference in this sample
        return OccurrenceDays()
            .Where(calculationRange.Contains)
            .SelectMany(a => this.Versions.EffectiveValueAt(a).DemandEntries)
            .GroupBy(a=> a.ProductId, 
                    (key, items) => 
                        new DemandItem(items.Sum(a=> a.Amount), items.First().UnitOfMeasure, key))
            .ToList();
    }
    public List<DemandsInDay> DemandsInRange(DateRange calculationRange)
    {
        return OccurrenceDays()
            .Where(calculationRange.Contains)
            .Select(a => new DemandsInDay(a, this.Versions.EffectiveValueAt(a).DemandEntries))
            .ToList();
    }

    public void ChangePlan(List<DayOfWeek> scheduleDays, List<DemandItem> demandEntries, IClock clock)
    {
        var newVersion = new SupplyPlanVersion(scheduleDays, demandEntries);
        this.Versions.Set(newVersion, clock.Now());
    }
}
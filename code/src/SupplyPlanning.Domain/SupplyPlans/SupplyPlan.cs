using System.Security.Principal;
using SupplyPlanning.Domain.Core;

namespace SupplyPlanning.Domain.SupplyPlans;

public class SupplyPlan
{
    public SupplyPlanId Id { get; private set; }
    public string Title { get; private set; }
    public DateRange ActivePeriod { get; private set; }
    public List<DayOfWeek> ScheduleDays { get; private set; }
    public List<DemandItem> DemandEntries { get; private set; }
    public SupplyPlan(string title, DateRange activePeriod, List<DayOfWeek> scheduleDays, List<DemandItem> demandEntries)
    {
        this.Id = SupplyPlanId.New();
        Title = title;
        ActivePeriod = activePeriod;
        ScheduleDays = scheduleDays;
        DemandEntries = demandEntries;
    }

    public IEnumerable<DateTime> OccurrenceDays()
    {
        return ActivePeriod.Days().Where(a => this.ScheduleDays.Contains(a.DayOfWeek));
    }

    public List<DemandItem> TotalDemandsInRange(DateRange calculationRange)
    {
        var count = OccurrenceDays().Where(calculationRange.Contains).Count();
        return this.DemandEntries
            .Select(a => new DemandItem(a.Amount * count, a.UnitOfMeasure, a.ProductId))
            .ToList();
    }

    public List<DemandsInDay> DemandsInRange(DateRange calculationRange)
    {
        return OccurrenceDays()
            .Where(calculationRange.Contains)
            .Select(a => new DemandsInDay(a, DemandEntries))
            .ToList();
    }
}
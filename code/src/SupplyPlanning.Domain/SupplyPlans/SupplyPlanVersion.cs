using SupplyPlanning.Domain.Core;

namespace SupplyPlanning.Domain.SupplyPlans;

public class SupplyPlanVersion
{
    public List<DayOfWeek> ScheduleDays { get; private set; }
    public List<DemandItem> DemandEntries { get; private set; }
    public SupplyPlanVersion(List<DayOfWeek> scheduleDays, List<DemandItem> demandEntries)
    {
        ScheduleDays = scheduleDays;
        DemandEntries = demandEntries;
    }

    public void CorrectPlan(List<DayOfWeek> scheduleDays, List<DemandItem> demandEntries)
    {
        this.ScheduleDays = scheduleDays;
        this.DemandEntries = demandEntries;
    }
}
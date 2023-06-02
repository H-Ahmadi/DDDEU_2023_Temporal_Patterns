using SupplyPlanning.Domain.Core;

namespace SupplyPlanning.Domain.SupplyPlans;

public class DemandsInDay
{
    public DateTime Date { get; private set; }
    public List<DemandItem> DemandItems { get; private set; }
    public DemandsInDay(DateTime date, List<DemandItem> demandItems)
    {
        Date = date;
        DemandItems = demandItems;
    }
    protected bool Equals(DemandsInDay other)
    {
        return Date.Equals(other.Date) && DemandItems.ElementsAreEqualTo(other.DemandItems);
    }
}
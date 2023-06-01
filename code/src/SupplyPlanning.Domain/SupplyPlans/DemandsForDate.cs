namespace SupplyPlanning.Domain.SupplyPlans;

public class DemandsForDate
{
    public DateTime Date { get; private set; }
    public List<DemandItem> DemandItems { get; private set; }
    public DemandsForDate(DateTime date, List<DemandItem> demandItems)
    {
        Date = date;
        DemandItems = demandItems;
    }
    protected bool Equals(DemandsForDate other)
    {
        return Date.Equals(other.Date) && DemandItems.Equals(other.DemandItems);
    }
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((DemandsForDate)obj);
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Date, DemandItems);
    }
}
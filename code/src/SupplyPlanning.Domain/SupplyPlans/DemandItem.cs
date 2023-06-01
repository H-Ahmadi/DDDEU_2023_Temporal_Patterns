namespace SupplyPlanning.Domain.SupplyPlans;

public class DemandItem
{
    public double Amount { get; set; }
    public string UnitOfMeasure { get; set; }
    public long ProductId { get; set; }
    public DemandItem(double amount, string unitOfMeasure, long productId)
    {
        Amount = amount;
        UnitOfMeasure = unitOfMeasure;
        ProductId = productId;
    }
    protected bool Equals(DemandItem other)
    {
        return Amount.Equals(other.Amount) && UnitOfMeasure == other.UnitOfMeasure && ProductId == other.ProductId;
    }
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((DemandItem)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Amount, UnitOfMeasure, ProductId);
    }
}
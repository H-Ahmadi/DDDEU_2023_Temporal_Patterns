using SupplyPlanning.Domain.Core;

namespace SupplyPlanning.Domain.SupplyPlans;

public class DemandItem : ValueObject
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

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return UnitOfMeasure;
        yield return ProductId;
    }
}
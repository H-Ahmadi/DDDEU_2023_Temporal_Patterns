using SupplyPlanning.Domain.Core;

namespace SupplyPlanning.Domain.SupplyPlans;

public class SupplyPlanId : ValueObject
{
    public Guid Value { get; private set; }
    public SupplyPlanId(Guid value)
    {
        Value = value;
    }
    public static SupplyPlanId New() => new SupplyPlanId(Guid.NewGuid());
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
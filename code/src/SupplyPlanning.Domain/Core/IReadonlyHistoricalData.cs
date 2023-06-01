namespace SupplyPlanning.Domain.Core;

public interface IReadonlyHistoricalData<T>
{
    public T? GetValue();
    T? EffectiveValueAt(DateTime effectiveSince);
}
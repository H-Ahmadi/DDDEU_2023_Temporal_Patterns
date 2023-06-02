namespace SupplyPlanning.Domain.Core;

public class HistoricalData<T> : IReadonlyHistoricalData<T>
{
    private List<TemporalValue<T>> _items;
    public IReadOnlyList<TemporalValue<T>> Items => _items;
    public HistoricalData()
    {
        _items = new List<TemporalValue<T>>();
    }
    public void Set(T item, DateTime effectiveSince)
    {
        _items.Add(new TemporalValue<T>(item, effectiveSince));
    }
    public T? GetValue()
    {
        if (_items.Any())
            return _items.OrderBy(a => a.EffectiveSince).Last().Value;
        return default;
    }
    public T? EffectiveValueAt(DateTime effectiveSince)
    {
        //TODO: consider if client is asking for an effectiveDate which is even before the first one
        if (_items.Any())
            return Items
                .Where(a => a.EffectiveSince <= effectiveSince)
                .OrderBy(a => a.EffectiveSince)
                .Last().Value;

        return default;
    }

    public static implicit operator T?(HistoricalData<T> record)
    {
        return record.GetValue();
    }
}
namespace SupplyPlanning.Domain.Core;

public interface ICloneable<T>
{
    T Clone();
}
namespace SupplyPlanning.Domain.Core;

public static class ValueObjectExtensions
{
    public static bool ElementsAreEqualTo<T>(this List<T> left, List<T> right) where T : ValueObject
    {
        return left.Except(right).Any() && right.Except(left).Any();
    }
}
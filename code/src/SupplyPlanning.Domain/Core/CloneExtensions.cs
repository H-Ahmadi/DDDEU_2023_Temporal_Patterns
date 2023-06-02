namespace SupplyPlanning.Domain.Core;

public static class CloneExtensions
{
    public static List<T> Clone<T>(this List<T> source) where T : ICloneable<T>
    {
        return source.Select(a => a.Clone()).ToList();
    }
}
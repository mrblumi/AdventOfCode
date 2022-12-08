namespace AdventOfCode.Extensions;

public static class EnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> self, Action<T> action)
    {
        foreach (var item in self) action.Invoke(item);
    }
}
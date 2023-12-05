namespace AdventOfCode.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T[]> Batch<T>(this IEnumerable<T> source, int batchSize)
    {
        var batch = new T[batchSize];
        var currentSize = 0;

        foreach (var item in source)
        {
            batch[currentSize++] = item;

            if (currentSize < batchSize) continue;
            yield return batch;
            
            batch = new T[batchSize];
            currentSize = 0;
        }

        if (currentSize <= 0) yield break;
        
        Array.Resize(ref batch, currentSize);
        yield return batch;
    }
    
    public static void ForEach<T>(this IEnumerable<T> self, Action<T> action)
    {
        foreach (var item in self) action.Invoke(item);
    }
}
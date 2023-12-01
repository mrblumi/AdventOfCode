namespace AdventOfCode.Extensions;

public static class ArrayExtensions
{
    public static IEnumerable<T[]> Permutations<T>(this T[] source) =>
        source.Permutations(0);

    private static IEnumerable<T[]> Permutations<T>(this T[] source, int i)
    {
        if (i == source.Length) yield return source.ToArray();
        
        for (var j = i; j < source.Length; j++) {
            source.Swap(i, j);
            foreach (var permutation in source.Permutations(i + 1)) yield return permutation;
            source.Swap(i, j);
        }
    }
    
    private static void Swap<T>(this T[] self, int i, int j) =>
        (self[i], self[j]) = (self[j], self[i]);
}
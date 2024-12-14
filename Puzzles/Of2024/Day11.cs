using System.Numerics;

namespace AdventOfCode.Puzzles.Of2024;

[Puzzle(2024, 11, "Plutonian Pebbles")]
public class Day11 : Puzzle<BigInteger>
{
    private readonly Dictionary<long, long> _stones;

    public Day11() => _stones = InputText
        .Split(' ')
        .Select(long.Parse)
        .ToLookup(x => x)
        .ToDictionary(x => x.Key, x => (long)x.Count());

    protected override BigInteger PartOne() => CountStones(_stones, blinks: 25);
    protected override BigInteger PartTwo() => CountStones(_stones, blinks: 75);

    private static BigInteger CountStones(IReadOnlyDictionary<long, long> stones, int blinks) =>
        blinks > 0
            ? CountStones(Blink(stones), blinks - 1)
            : stones.SumBy<KeyValuePair<long, long>, BigInteger>(x => x.Value);

    private static IReadOnlyDictionary<long, long> Blink(IReadOnlyDictionary<long, long> current)
    {
        var next = new Dictionary<long, long>();

        foreach (var pair in current)
        foreach (var value in Blink(pair.Key))
            if (next.ContainsKey(value)) next[value] += pair.Value;
            else next[value] = pair.Value;

        return next;
    }
    
    private static long[] Blink(long stone)
    {
        if (stone is 0) return [1];

        var numberOfDigits = GetNumberOfDigits(stone);
        var divisor = GetDivisor(numberOfDigits);
        
        if (numberOfDigits % 2 is 0) return [ stone / divisor, stone % divisor ];

        return [stone * 2024];
    }

    private static int GetNumberOfDigits(long number) =>
        (int)System.Math.Log10(number) + 1;

    private static long GetDivisor(int numberOfDigits) =>
        (long)System.Math.Pow(10, numberOfDigits / 2);
}
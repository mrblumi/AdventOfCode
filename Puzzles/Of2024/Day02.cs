using Math = System.Math;

namespace AdventOfCode.Puzzles.Of2024;

[Puzzle(2024, 02, "Red-Nosed Reports")]
public class Day02 : Puzzle<int>
{
    private readonly int[][] _reports;

    public Day02() => _reports = InputLines
        .Select(x => x
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(Parse)
            .ToArray())
        .ToArray();

    protected override int PartOne() => _reports.Count(IsSafe);
    protected override int PartTwo() => _reports.Count(IsSafeWithBadValue);

    private static bool IsSafe(int[] items)
    {
        var signum = 0;
        
        for (var i = 1; i < items.Length; i++)
        {
            var difference = items[i - 1] - items[i];
            if (signum == 0) signum = Math.Sign(difference);

            if (Math.Abs(difference) is 0 or > 3) return false;
            if (Math.Sign(difference) - signum is not 0) return false;
        }

        return true;
    }

    private static bool IsSafeWithBadValue(int[] items) => items
        .PowerSet()
        .Where(x => x.Length >= items.Length - 1)
        .Any(IsSafe);
}   
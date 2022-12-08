namespace AdventOfCode.Puzzles.Of2022;

[Puzzle(2022, 01, "Calorie Counting")]
public sealed class Day01 : Puzzle<int>
{
    private readonly IEnumerable<int> _amountOfCaloriesPerElf;

    public Day01() => _amountOfCaloriesPerElf = InputText
        .Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
        .Select(_ => _
            .Split("\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(Parse)
            .Sum())
        .ToArray();

    protected override int PartOne() => _amountOfCaloriesPerElf.Max();
    protected override int PartTwo() => _amountOfCaloriesPerElf.OrderDescending().Take(3).Sum();
}
namespace AdventOfCode.Puzzles.Of2022;

public sealed record Day01 : Puzzle
{
    private readonly IEnumerable<int> _amountOfCaloriesPerElf;

    public Day01() : base(Year: 2022, Day: 01, "Calorie Counting")
    {
        _amountOfCaloriesPerElf = InputText
            .Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
            .Select(_ => _
                .Split("\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .Sum())
            .ToArray();
    }

    protected override object PartOne() => _amountOfCaloriesPerElf.Max();
    protected override object PartTwo() => _amountOfCaloriesPerElf.OrderDescending().Take(3).Sum();
}
namespace AdventOfCode.Puzzles.Of2024;

[Puzzle(2024, 01, "Historian Hysteria")]
public class Day01 : Puzzle<int>
{
    private readonly List<int> _firstList = new();
    private readonly List<int> _secondList = new();

    public Day01() => InputLines
        .Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries))
        .ForEach(x =>
        {
            _firstList.Add(Parse(x[0]));
            _secondList.Add(Parse(x[1]));
        });

    protected override int PartOne() => Enumerable
        .Zip(
            _firstList.Order(),
            _secondList.Order())
        .Select(x => Abs(x.First - x.Second))
        .Sum();

    protected override int PartTwo() => _firstList
        .Select(x => _secondList.Count(y => y == x) * x)
        .Sum();
}
namespace AdventOfCode.Puzzles.Of2024;

[Puzzle(2024, 05, "Print Queue")]
public class Day05 : Puzzle<int>, IComparer<string>
{
    private readonly (string, string)[] _rules;
    private readonly string[][] _pages;
    
    public Day05()
    {
        _rules = InputLines
            .TakeWhile(x => x is not "")
            .Select(x => x.Split('|'))
            .Select(x => (x[0], x[1]))
            .ToArray();

        _pages = InputLines
            .SkipWhile(x => x is not "")
            .Skip(1)
            .Select(x => x.Split(','))
            .ToArray();
    }

    
    public int Compare(string? x, string? y)
    {
        if (_rules!.Contains((x,y))) return -1;
        if (_rules!.Contains((y,x))) return 1;
        return 0;
    }

    protected override int PartOne() => Solve(correctOrder: true);
    protected override int PartTwo() => Solve(correctOrder: false);

    private int Solve(bool correctOrder) => _pages
        .Select(x => (OriginalOrder: x, CorrectOrder: x.Order(this).ToArray()))
        .Where(x => Enumerable.SequenceEqual(x.OriginalOrder, x.CorrectOrder) == correctOrder)
        .Select(x => x.CorrectOrder)
        .Select(x => x[x.Length / 2])
        .Select(Parse)
        .Sum();
}
namespace AdventOfCode.Puzzles.Of2022;

public sealed record Day04 : Puzzle
{
    private readonly ((int Start, int End) First, (int Start, int End) Second)[] _tuples;

    public Day04() : base(Year: 2022, Day: 04, "Camp Cleanup") => _tuples = InputLines
        .Select(_ => _.Split(',', '-').Select(int.Parse).ToArray())
        .Select(_ => (First: (Start: _[0], End: _[1]), Second: (Start: _[2], End: _[3])))
        .ToArray();
    
    private int Solve(Func<bool,bool,bool> evaluate) => _tuples.Count(_ =>
        evaluate(
            _.First.Start >=_.Second.Start && _.First.Start <=_.Second.End,
            _.First.End >=_.Second.Start && _.First.End <= _.Second.End) ||
        evaluate(
            _.Second.Start >= _.First.Start && _.Second.Start <= _.First.End,
            _.Second.End >= _.First.Start && _.Second.End <= _.First.End));

    protected override int PartOne() => Solve(evaluate: (left, right) => left && right);

    protected override int PartTwo() => Solve(evaluate: (left, right) => left || right);
}
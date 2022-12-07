namespace AdventOfCode.Puzzles.Of2022;

public sealed record Day04 : Puzzle
{
    private readonly int[][] _tuples;

    public Day04() : base(Year: 2022, Day: 04, "Camp Cleanup") =>
        _tuples = InputLines.Select(_ => _.Split(',', '-').Select(int.Parse).ToArray()).ToArray();
    
    private int Solve(Func<bool,bool,bool> evaluate) => _tuples.Count(_ =>
        evaluate(_[0] >=_[2] && _[0] <=_[3], _[1] >=_[2] && _[1] <= _[3]) ||
        evaluate(_[2] >= _[0] && _[2] <= _[1], _[3] >= _[0] && _[3] <= _[1]));

    protected override object PartOne() => Solve(evaluate: (left, right) => left && right);
    protected override object PartTwo() => Solve(evaluate: (left, right) => left || right);
}
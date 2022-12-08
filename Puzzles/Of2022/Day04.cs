namespace AdventOfCode.Puzzles.Of2022;

[Puzzle(2022, 04, "Camp Cleanup")]
public sealed class Day04 : Puzzle<int>
{
    private readonly int[][] _tuples;
    public Day04() => _tuples = InputLines.Select(_ => _.Split(',', '-').Select(Parse).ToArray()).ToArray();
    
    private int Solve(Func<bool,bool,bool> evaluate) => _tuples.Count(_ =>
        evaluate(_[0] >=_[2] && _[0] <=_[3], _[1] >=_[2] && _[1] <= _[3]) ||
        evaluate(_[2] >= _[0] && _[2] <= _[1], _[3] >= _[0] && _[3] <= _[1]));

    protected override int PartOne() => Solve(evaluate: (left, right) => left && right);
    protected override int PartTwo() => Solve(evaluate: (left, right) => left || right);
}
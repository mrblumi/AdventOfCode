namespace AdventOfCode.Puzzles.Of2022;

[Puzzle(2022, 06, "Tuning Trouble")]
public sealed class Day06 : Puzzle<int>
{
    private int Solve(int distinct) => Enumerable
        .Range(0, MaxValue)
        .First(_ => InputText.Substring(_, distinct).Distinct().Count() == distinct) + distinct;
    
    protected override int PartOne() => Solve(distinct: 4);
    protected override int PartTwo() => Solve(distinct: 14);
}
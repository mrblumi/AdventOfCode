namespace AdventOfCode.Puzzles.Of2015;

[Puzzle(2015, 17, "No Such Thing as Too Much")]
public class Day17 : Puzzle<int>
{
    private readonly IReadOnlyCollection<int[]> _containersPowerSet;

    public Day17()
    {
        var containers = InputLines
            .Select(Parse)
            .ToArray();
        
        _containersPowerSet = containers
            .PowerSet()
            .ToArray();
    }

    protected override int PartOne() =>
        CombinationsWith(liters: 150).Count();

    protected override int PartTwo() =>
        CombinationsWith(liters: 150).GroupBy(x => x.Length).OrderBy(x => x.Key).First().Count();

    private IEnumerable<int[]> CombinationsWith(int liters) => _containersPowerSet.Where(x => x.Sum() == liters);
}
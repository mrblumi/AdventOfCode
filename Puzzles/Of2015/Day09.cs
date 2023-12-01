namespace AdventOfCode.Puzzles.Of2015;

[Puzzle(2015, 09, "All in a Single Night")]
public class Day09 : Puzzle<int>
{
    private readonly IReadOnlyCollection<int> _distances;
    
    public Day09()
    {
        var routes = InputLines
            .Select(_ => _.Split(' '))
            .Select(_ => new { Route = new Route(_[0], _[2]), Distance = Parse(_[4]) })
            .ToArray();
        var locations = routes
            .SelectMany(_ => _.Route.Locations)
            .Distinct()
            .ToArray();
        var map = routes
            .ToDictionary(_ => _.Route, _ => _.Distance);

        _distances = locations
            .Permutations()
            .Select(stops => stops
                .Zip(stops[1..])
                .Select(route => new Route(route.First, route.Second))
                .Select(route => map[route])
                .Sum())
            .ToArray();
    }

    protected override int PartOne() => _distances.Min();
    protected override int PartTwo() => _distances.Max();
    
    public sealed record Route(string Start, string Destination)
    {
        public IEnumerable<string> Locations { get { yield return Start; yield return Destination; } }

        public bool Equals(Route? other) =>
            other is not null && Start == other.Start && Destination == other.Destination ||
            other is not null && Start == other.Destination && Destination == other.Start;

        public override int GetHashCode() => 0;
    }
}
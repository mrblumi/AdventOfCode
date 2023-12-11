namespace AdventOfCode.Puzzles.Of2022;

using static System.Math;

[Puzzle(2022, 14, "Regolith Reservoir")]
public class Day14 : Puzzle<int>
{
    private readonly IReadOnlyCollection<Coordinate> _scan;
    
    public Day14()
    {
        _scan =  InputLines
            .Select(Edges)
            .SelectMany(Rocks)
            .Distinct()
            .ToArray();
        
        Coordinate[] Edges(string line) => line
            .Split(" -> ")
            .Select(_ => _.Split(','))
            .Select(_ => new Coordinate(Parse(_[0]), Parse(_[1])))
            .ToArray();    

        IEnumerable<Coordinate> Rocks(Coordinate[] edges) => Enumerable
            .Zip(edges[..^1], edges[1..])
            .SelectMany(_ => Bolders(_.First, _.Second));

        IEnumerable<Coordinate> Bolders(Coordinate a, Coordinate b) => a.X == b.X
            ? Enumerable.Range(Min(a.Y, b.Y), Abs(b.Y - a.Y) + 1).Select(y => a with { Y = y })
            : Enumerable.Range(Min(a.X, b.X), Abs(b.X - a.X) + 1).Select(x => a with { X = x });
    }

    private bool DropSand(IDictionary<Coordinate, char> scan, Coordinate start)
    {
        var blocking = scan.Keys
            .Where(_ => _.X == start.X)
            .OrderBy(_ => _.Y)
            .FirstOrDefault(_ => _.Y >= start.Y);

        if (blocking is null) return false;
        if (blocking == start) return false;

        var left = blocking with { X = blocking.X - 1 };
        if (!scan.ContainsKey(left)) return DropSand(scan, left);
        
        var right = blocking with { X = blocking.X + 1 };
        if (!scan.ContainsKey(right)) return DropSand(scan, right);

        scan[blocking with { Y = blocking.Y - 1 }] = 'o';
        return true;
    }

    private int DropSand(IDictionary<Coordinate, char> scan)
    {
        var sandSource = new Coordinate(500, 0);
        var count = 0;

        while (DropSand(scan, sandSource)) count++;

        return count;
    }

    protected override int PartOne() => DropSand(_scan.ToDictionary(_ => _, _ => '#'));
    
    protected override int PartTwo()
    {
        var scan = _scan.ToDictionary(_ => _, _ => '#');
        var lines = scan.Keys.OrderBy(_ => _.Y) .Last().Y;

        Enumerable.Range(250, 500).ForEach(_ => scan[new(_, lines + 2)] = '#');

        return DropSand(scan);
    }
}
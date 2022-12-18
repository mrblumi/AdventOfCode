namespace AdventOfCode.Puzzles.Of2022;

[Puzzle(2022, 18, "Boiling Boulders")]
public class Day18 : Puzzle<int>
{
    private record struct Coordinate(int X, int Y, int Z)
    {
        public IEnumerable<Coordinate> Neighbours()
        {
            yield return this with { X = X - 1 };
            yield return this with { X = X + 1 };
            yield return this with { Y = Y - 1 };
            yield return this with { Y = Y + 1 };
            yield return this with { Z = Z - 1 };
            yield return this with { Z = Z + 1 };
        }
    }

    private record struct Volume(Coordinate Start, Coordinate End)
    {
        public bool Contains(Coordinate c)
            => Start.X <= c.X && c.X <= End.X 
            && Start.Y <= c.Y && c.Y <= End.Y
            && Start.Z <= c.Z && c.Z <= End.Z;
    }

    private readonly Coordinate[] _lava;

    public Day18() => _lava = InputLines
        .Select(_ => _.Split(',').Select(Parse).ToArray())
        .Select(_ => new Coordinate(_[0], _[1], _[2]))
        .ToArray();

    protected override int PartOne() => _lava
        .SelectMany(_ => _.Neighbours())
        .Count(_ => !_lava.Contains(_));

    protected override int PartTwo()
    {
        var areaOfInterest = new Volume(
            Start: new(_lava.MinBy(_ => _.X).X - 1, _lava.MinBy(_ => _.Y).Y - 1, _lava.MinBy(_ => _.Y).Y - 1),
            End:   new(_lava.MaxBy(_ => _.X).X + 1, _lava.MaxBy(_ => _.Y).Y + 1, _lava.MaxBy(_ => _.Y).Y + 1));

        var water = new HashSet<Coordinate>();
        var queue = new Queue<Coordinate>();

        water.Add(areaOfInterest.Start);
        queue.Enqueue(areaOfInterest.Start);

        while (queue.TryDequeue(out var coordinate))
            foreach (var neighbour in coordinate.Neighbours())
            {
                if(water.Contains(neighbour)) continue;
                if(_lava.Contains(neighbour)) continue;

                if (areaOfInterest.Contains(neighbour))
                {
                    queue.Enqueue(neighbour);
                    water.Add(neighbour);
                }
            }

        return _lava.SelectMany((_ => _.Neighbours())).Count(water.Contains);
    }
}
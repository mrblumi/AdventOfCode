using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzles.Of2022;

using static System.Math;

[Puzzle(2022, 15, "Beacon Exclusion Zone")]
public partial class Day15 : Puzzle<int, long>
{
    private record Sensor(Coordinate Position, Coordinate Beacon)
    {
        public int Dimension { get; } = Distance(Position, Beacon);

        public int Distance(Coordinate other) => Distance(Position, other);
        private static int Distance(Coordinate a, Coordinate b) => Abs(a.X - b.X) + Abs(a.Y - b.Y);
    }

    private readonly Sensor[] _sensors;
    
    public Day15() => _sensors = InputLines
        .Select(_ => Regex().Match(_))
        .Select(_ => new Sensor(
            Position: new(Parse(_.Groups["SX"].Value), Parse(_.Groups["SY"].Value)),
            Beacon: new(Parse(_.Groups["BX"].Value), Parse(_.Groups["BY"].Value))))
        .ToArray();

    [GeneratedRegex(@".*x=(?<SX>-{0,1}\d+), y=(?<SY>-{0,1}\d+):.*x=(?<BX>-{0,1}\d+), y=(?<BY>-{0,1}\d+)")]
    private partial Regex Regex(); 
    
    protected override int PartOne()
    {
        const int line = 2000000;

        var beacons = _sensors.Select(_ => _.Beacon);
        var coveredFields = _sensors
            .Where(_ => _.Position.Y - _.Dimension <= line && line <= _.Position.Y + _.Dimension)
            .SelectMany(CoveredFields);

        return coveredFields.Except(beacons).Count();
            
        static IEnumerable<Coordinate> CoveredFields(Sensor s)
        {
            for (var x = s.Position.X - s.Dimension; x <= s.Position.X + s.Dimension; x++)
                if (s.Distance(new(x, line)) <= s.Dimension)
                    yield return new(x, line);
        }
    }

    protected override long PartTwo()
    {
        const long dimension = 4000000;

        var candidate = _sensors.SelectMany(OuterBorder)
            .Where(_ => 0 <= _.X && _.X <= dimension && 0 <= _.Y && _.Y <= dimension)
            .Where(_ => _sensors.All(s => s.Distance(_) > s.Dimension))
            .Distinct()
            .Single();

        return candidate.X * dimension + candidate.Y;

        static IEnumerable<Coordinate> OuterBorder(Sensor s)
        {
            var dimension = s.Dimension + 1;
            for (var i = 0; i < dimension; i++)
            {
                yield return new(s.Position.X - dimension + i, s.Position.Y + i);
                yield return new(s.Position.X + i, s.Position.Y + dimension - i);
                yield return new(s.Position.X + dimension - i, s.Position.Y + i);
                yield return new(s.Position.X - i, s.Position.Y - dimension + i);
            }
        }
    }
}
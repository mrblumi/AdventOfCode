using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzles.Of2015;

public sealed record Day06 : Puzzle
{
    private static readonly Regex Regex = new(
        pattern: @"^(?<Method>.*) (?<Start>\d*,\d*) through (?<End>\d*,\d*)",
        options: RegexOptions.Compiled);

    private readonly Match[] _matches;
    
    public Day06() : base(Year: 2015, Day: 06, "Probably a Fire Hazard") =>
        _matches = InputLines.Select(_ => Regex.Match(_)).ToArray();


    protected override int PartOne() => Solve(
        getResult: lights => lights.Count(_ => _.Brightness > 0),
        getAction: command => command switch
        {
            "turn on" => l => l.Brightness = 1,
            "turn off" => l => l.Brightness = 0,
            "toggle" => l => l.Brightness = l.Brightness != 0 ? 0 : 1,
            _ => throw new NotSupportedException(command)
        });
    
    protected override int PartTwo() => Solve(
        getResult: lights => lights.Sum(_ => _.Brightness),
        getAction: command => command switch
        {
            "turn on" => l => l.Brightness++,
            "turn off" => l => l.Brightness--,
            "toggle" => l => l.Brightness += 2,
            _ => throw new NotSupportedException(command)
        });

    private int Solve(Func<IEnumerable<Light>, int> getResult, Func<string, Action<Light>> getAction)
    {
        var executionPlan = _matches.Select(ExecutionPlan.With(getAction));
        var lights = Enumerable
            .Range(0, 1000000)
            .Select(i => new Light(X: i % 1000, Y: i / 1000))
            .ToArray();
        
        executionPlan.ForEach(_ => _.ExecuteOn(lights));
        return getResult(lights);
    }

    private record ExecutionPlan(Coordinate Start, Coordinate End, Action<Light> Action)
    {
        public static Func<Match, ExecutionPlan> With(Func<string, Action<Light>> getAction) => match =>
            new(
                Start: Coordinate.Parse(match.Groups["Start"].Value),
                End: Coordinate.Parse(match.Groups["End"].Value),
                Action: getAction(match.Groups["Method"].Value));
        
        public void ExecuteOn(IEnumerable<Light> lights) =>
            lights.Where(_ => _.X.IsBetween(Start.X, End.X) && _.Y.IsBetween(Start.Y, End.Y)).ForEach(Action);
    }

    private record Coordinate(int X, int Y)
    {
        public static Coordinate Parse(string s)
        {
            var parts = s.Split(',');
            return new(int.Parse(parts[0]), int.Parse(parts[1]));
        }
    }
    
    private record Light(int X, int Y)
    {
        private int _brightness = 0;
        public int Brightness { get => _brightness; set => _brightness = Math.Max(value, 0); }
    }
}
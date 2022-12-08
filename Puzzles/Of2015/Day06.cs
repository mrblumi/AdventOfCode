namespace AdventOfCode.Puzzles.Of2015;

using static Math;

[Puzzle(2015, 06, "Probably a Fire Hazard")]
public sealed class Day06 : Puzzle<int>
{
    private record struct Point(int X, int Y);
    private record struct Light(Point Point, int Brightness = 0);
    private record struct Instruction(Point Start, Point End, Func<int, int> Action);
    
    private static Point Parse(Span<string> s) => new(int.Parse(s[0]), int.Parse(s[1]));

    private static IEnumerable<Light> Seed => Enumerable
        .Range(0, 1000000)
        .Select(_ => new Light(Point: new Point(X: _ % 1000, Y: _ / 1000)));

    private static Func<Light, Light> OnNext(Instruction plan) => light =>
        light.Point.X >= plan.Start.X && light.Point.X <= plan.End.X &&
        light.Point.Y >= plan.Start.Y && light.Point.Y <= plan.End.Y
            ? light with { Brightness = plan.Action.Invoke(light.Brightness) }
            : light;

    private static IEnumerable<Light> OnNext(IEnumerable<Light> lights, Instruction plan) => lights.Select(OnNext(plan));
    
    private int Solve(Func<int, int> turnOn, Func<int, int> turnOff, Func<int, int> toggle) => InputLines
        .Select(_ => _.Split(' ', ','))
        .Select(_ => _.AsSpan() switch
        {
            [_, "on", .. var span] => new Instruction(Parse(span[..2]), Parse(span[3..5]), turnOn),
            [_, "off", .. var span] => new Instruction(Parse(span[..2]), Parse(span[3..5]), turnOff),
            ["toggle", .. var span] => new Instruction(Parse(span[..2]), Parse(span[3..5]), toggle),
            _ => throw new NotSupportedException()
        })
        .Aggregate(seed: Seed, func: OnNext)
        .Sum(_ => _.Brightness);
    
    protected override int PartOne() => Solve(_ => 1, _ => 0, _ => 1 - _);
    protected override int PartTwo() => Solve(_ => _ + 1, _ => Max(0, _ - 1), _ => _ + 2);
}
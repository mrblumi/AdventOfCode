namespace AdventOfCode.Puzzles.Of2023;

[Puzzle(2023, 02, "Cube Conundrum")]
public class Day02 : Puzzle<int>
{
    private readonly IReadOnlyCollection<Game> _games;

    public Day02() => _games = InputLines
        .Select((l,i) => new Game(
            Id: i + 1,
            Information: l
                .Split(": ")[1]
                .Split("; ")
                .Select(g => new Information(
                    Cubes: g
                        .Split(", ")
                        .Select(c => c.Split(" "))
                        .Select(c => new Cubes(c[1], Parse(c[0])))
                        .ToArray()))
                .ToArray()))
        .ToArray();

    private sealed record Game(int Id, IReadOnlyCollection<Information> Information);
    private sealed record Information(IReadOnlyCollection<Cubes> Cubes);
    private sealed record Cubes(string Color, int Amount);

    protected override int PartOne()
    {
        var cubes = new Dictionary<string, int> { { "red", 12 }, { "green", 13 }, { "blue", 14 } };
        
        return _games
            .Where(g => g.Information.All(i => i.Cubes.All(c => cubes[c.Color] >= c.Amount)))
            .Select(g => g.Id)
            .Sum();
    }

    protected override int PartTwo() => _games
        .Select(g => g.Information
            .SelectMany(i => i.Cubes)
            .GroupBy(c => c.Color, c => c.Amount)
            .Select(c => c.Max())
            .Aggregate(1, (x, y) => x * y))
        .Sum();
}
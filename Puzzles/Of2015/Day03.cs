namespace AdventOfCode.Puzzles.Of2015;

[Puzzle(2015, 03, "Perfectly Spherical Houses in a Vacuum")]
public sealed class Day03 : Puzzle<int>
{
    protected override int PartOne() => Solve(santas: 1);
    protected override int PartTwo()  => Solve(santas: 2);

    private int Solve(int santas)
    {
        var locations = new Location?[santas];
        var visitedHouses = new HashSet<Location> { new() };
        var currentSanta = 0;
        
        foreach (var instruction in InputText)
        {
            visitedHouses.Add(locations[currentSanta] = (locations[currentSanta] ?? new()).OnNext(instruction));
            currentSanta = (currentSanta + 1) % santas;
        }

        return visitedHouses.Count;
    }
    
    private sealed record Location(int X = 0, int Y = 0)
    {
        public Location OnNext(char instruction) => instruction switch
        {
            '^' => this with { Y = Y + 1 },
            'v' => this with { Y = Y - 1 },
            '>' => this with { X = X + 1 },
            '<' => this with { X = X - 1 },
            _ => throw new NotSupportedException()
        };
    }
}
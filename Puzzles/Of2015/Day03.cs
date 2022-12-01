namespace AdventOfCode.Puzzles.Of2015;

public sealed record Day03() : Puzzle(Year: 2015, Day: 03, "Perfectly Spherical Houses in a Vacuum")
{
    protected override int PartOne()
    {
        var location = new Location();
        var hashSet = new HashSet<Location> { location };

        foreach (var instruction in InputText)
        {
            location = location.OnNext(instruction);
            hashSet.Add(location);
        }

        return hashSet.Count;
    }

    protected override int PartTwo()
    {
        var location = new Location();
        var santasLocation = location;
        var robotsLocation = location;
        
        var hashSet = new HashSet<Location> { location };

        for (var i = 0; i < InputText.Length; i++)
        {
            if (i % 2 == 0)
            {
                santasLocation = santasLocation.OnNext(InputText[i]);
                hashSet.Add(santasLocation);
            }
            else
            {
                robotsLocation = robotsLocation.OnNext(InputText[i]);
                hashSet.Add(robotsLocation);
            }
        }

        return hashSet.Count;
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
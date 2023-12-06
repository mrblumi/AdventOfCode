namespace AdventOfCode.Puzzles.Of2023;

[Puzzle(2023, 06, "Wait For It")]
public class Day06 : Puzzle<int>
{
    private readonly string[] _times;
    private readonly string[] _distances;
    
    public Day06()
    {
        _times = Read(0);
        _distances = Read(1);
    }

    private string[] Read(int line) => InputLines[line].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1..];

    private static int Calculate(double time, double distanceToBeat)
    {
        var squareRoot = Math.Sqrt(time * time - 4 * distanceToBeat);
        
        var floor = (time + squareRoot) / 2;
        var ceiling = (time - squareRoot) / 2;

        floor = Math.Floor(floor) - (floor % 1 == 0 ? 1 : 0);
        ceiling = Math.Ceiling(ceiling) + (ceiling % 1 == 0 ? 1 : 0);
        
        var result = (int)floor - (int)ceiling + 1;

        return result;
    }
    
    protected override int PartOne() => Enumerable
        .Zip(_times.Select(Parse), _distances.Select(Parse))
        .Select(pair => Calculate(pair.First, pair.Second))
        .Aggregate(1, (x, y) => x * y);

    protected override int PartTwo() => Calculate(
        time: long.Parse(string.Join(null, _times)), 
        distanceToBeat: long.Parse(string.Join(null, _distances)));
}
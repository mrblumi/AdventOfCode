namespace AdventOfCode.Commons;

public sealed class Map
{
    public static IReadOnlyDictionary<Coordinate, char> Parse(string[] inputLines)
    {
        var map = new Dictionary<Coordinate, char>();
        
        for (var y = 0; y < inputLines.Length; y++)
        for (var x = 0; x < inputLines[y].Length; x++)
            map.Add(new(x, y), inputLines[y][x]);

        return map;
    }
}
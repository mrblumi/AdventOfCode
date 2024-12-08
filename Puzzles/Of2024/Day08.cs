namespace AdventOfCode.Puzzles.Of2024;

[Puzzle(2024, 08, "Resonant Collinearity")]
public class Day08 : Puzzle<int>
{
    private readonly Dictionary<Coordinate, char> _map = new();
    
    public Day08()
    {
        for (var y = 0; y < InputLines.Length; y++)
        for (var x = 0; x < InputLines[y].Length; x++)
            _map.Add(new(x, y), InputLines[y][x]);
    }
    
    protected override int PartOne()
    {
        IEnumerable<Coordinate> Antinodes(Coordinate item1, Coordinate item2, Coordinate distance)
        {
            var left = item1 + distance;
            if (_map.ContainsKey(left)) yield return left;
            
            var right = item2 - distance;
            if (_map.ContainsKey(right)) yield return right;
        }
        
        return Solve(Antinodes);
    }

    protected override int PartTwo()
    {
        IEnumerable<Coordinate> Antinodes(Coordinate item1, Coordinate item2, Coordinate distance)
        {
            for (var i = 0 ;; i++)
            {
                var next = item1 + i * distance;
                if (_map.ContainsKey(next)) yield return next;
                else break;
            }
            
            for (var i = 0 ;; i++)
            {
                var next = item2 - i * distance;
                if (_map.ContainsKey(next)) yield return next;
                else break;
            }
        }
        
        return Solve(Antinodes);
    }

    private int Solve(Func<Coordinate, Coordinate, Coordinate, IEnumerable<Coordinate>> getAntinodes) => _map
        .Where(x => x.Value is not '.')
        .GroupBy(x => x.Value, x => x.Key)
        .Select(x => x.ToArray())
        .Select(x => x.Subsets(ofLength: 2))
        .SelectMany(x => x)
        .SelectMany(x => getAntinodes(x[0], x[1], x[0] - x[1]))
        .Distinct()
        .Count();
}
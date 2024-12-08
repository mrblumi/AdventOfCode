using System.Collections.Immutable;
using System.Numerics;

namespace AdventOfCode.Puzzles.Of2024;

[Puzzle(2024, 06, "Guard Gallivant")]
public class Day06 : Puzzle<int>
{
    private readonly ImmutableDictionary<Coordinate, char> _map;
    private readonly Coordinate _startPosition;
    private readonly Matrix _turnRight;
    
    public Day06()
    {
        
        var map = new Dictionary<Coordinate, char>();
        for (var y = 0; y < InputLines.Length; y++)
        for (var x = 0; x < InputLines[y].Length; x++)
            map.Add(new(x, y), InputLines[y][x]);

        _map = map.ToImmutableDictionary();
        _startPosition = _map.Single(x => x.Value == '^').Key;
        _turnRight =  new(0, -1, 1, 0);
    }
    
    protected override int PartOne()
        => GetGuardsRoute(_map, _startPosition).Visited.Count;

    protected override int PartTwo()
        => GetGuardsRoute(_map, _startPosition).Visited
            .Where(x => x != _startPosition)
            .Select(x => _map.SetItem(x, '#'))
            .Select(x => GetGuardsRoute(x, _startPosition))
            .Count(x => x.IsLoop);

    private Route GetGuardsRoute(ImmutableDictionary<Coordinate, char> map, Coordinate position)
    {
        var visited = new HashSet<(Coordinate Position, Coordinate Direction)>();
        var direction = new Coordinate(0, -1);
        
        while (map.ContainsKey(position) && visited.Add((position, direction)))
        {
            var next = position + direction;

            if (map.GetValueOrDefault(next) is '#') direction = _turnRight * direction;
            else position = next;
        }

        return new(
            Visited: visited.Select(x => x.Position).ToHashSet(),
            IsLoop: visited.Contains((position, direction)));
    }

    private readonly record struct Matrix(int X11, int X12, int X21, int X22)
    {
        public static Coordinate operator *(Matrix left, Coordinate right) =>
            new(left.X11 * right.X + left.X12 * right.Y, left.X21 * right.X + left.X22 * right.Y);
    }
    
    private sealed record Route(HashSet<Coordinate> Visited, bool IsLoop);
}
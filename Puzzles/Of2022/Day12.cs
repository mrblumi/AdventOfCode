namespace AdventOfCode.Puzzles.Of2022;

[Puzzle(2022, 12, "Hill Climbing Algorithm")]
public class Day12 : Puzzle<int>
{
    private record struct Elevation(char Value, char? Symbol = null);
    private record struct Path(Elevation Elevation, int Distance);
    
    private readonly IDictionary<Coordinate, Elevation> _map;
    private readonly IDictionary<Coordinate, Path> _paths;

    public Day12()
    {
        _map = InputLines
            .SelectMany((line, x) => line.Select((c, y) => (
                Coordinate: new Coordinate(x, y),
                Elevation: c switch { 'S' => new('a', 'S'), 'E' => new('z', 'E'), _ => new Elevation(c) })))
            .ToDictionary(_ => _.Coordinate, _ => _.Elevation);
        
        var end = _map.Single(_ => _.Value.Symbol == 'E');
        var paths = new Dictionary<Coordinate, Path> { [end.Key] = new(end.Value, 0) };
        var queue = new Queue<Coordinate>();

        queue.Enqueue(end.Key);

        while (queue.Count > 0)
        {
            var coordinate = queue.Dequeue();
            var path = paths[coordinate];

            foreach (var neighbour in _map.Keys.Where(to => 
                 (coordinate.X == to.X && Math.Abs(coordinate.Y - to.Y) == 1) ||
                 (coordinate.Y == to.Y && Math.Abs(coordinate.X - to.X) == 1)))
            {
                var next = _map[neighbour];

                if (paths.ContainsKey(neighbour)) continue;
                if (path.Elevation.Value - next.Value > 1) continue;

                paths[neighbour] = new(_map[neighbour], path.Distance + 1);
                queue.Enqueue(neighbour);
            }
        }

        _paths = paths;
    }

    protected override int PartOne()
    {
        var start = _map.Single(_ => _.Value.Symbol == 'S').Key;
        return _paths[start].Distance;
    }

    protected override int PartTwo()
    {
        var starts = _paths.Keys.Where(_ => _map[_].Value == 'a');
        return starts.Select(_ => _paths[_]).Min(_ => _.Distance);
    }
}
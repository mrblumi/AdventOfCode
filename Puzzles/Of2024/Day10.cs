namespace AdventOfCode.Puzzles.Of2024;

[Puzzle(2024, 10, "Hoof It")]
public class Day10 : Puzzle<int>
{
    private readonly Coordinate[] _directions = [new(1, 0), new(0, 1), new(-1, 0), new(0, -1)];
    private readonly IReadOnlyDictionary<Coordinate, char> _map;

    public Day10() => _map = Map.Parse(InputLines);

    protected override int PartOne() => Solve(x => GetTrailHeadScore(x, new(), (ref HashSet<Coordinate> y, Coordinate z) => y.Add(z)).Count);
    protected override int PartTwo() => Solve(x => GetTrailHeadScore(x, 0, (ref int y, Coordinate z) => y++));

    private int Solve(Func<Coordinate, int> getTrailHeadScore) => _map
        .Where(x => x.Value is '0')
        .Select(x => getTrailHeadScore(x.Key))
        .Sum();
    
    private delegate void Score<TScore>(ref TScore score, Coordinate coordinate);
    
    private TScore GetTrailHeadScore<TScore>(Coordinate trailhead, TScore score, Score<TScore> addToScore)
    {
        var queue = new Queue<Coordinate>([ trailhead ]);
        
        while (queue.TryDequeue(out var key))
        {
            _directions
                .Select(x => key + x)
                .Where(x => _map.ContainsKey(x))
                .Where(x => _map[x] == _map[key] + 1)
                .ForEach(queue.Enqueue);

            if (_map[key] is '9') addToScore.Invoke(ref score, key);
        }
        
        return score;
    }
}
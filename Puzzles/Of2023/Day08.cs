namespace AdventOfCode.Puzzles.Of2023;

using static Commons.Math;

[Puzzle(2023, 08, "Haunted Wasteland")]
public class Day08 : Puzzle<long>
{
    private readonly string _directions;
    private readonly IDictionary<string, (string Left, string Right)> _map;

    public Day08()
    {
        _directions = InputLines[0];
        _map = InputLines[2..]
            .ToDictionary(
                keySelector: line => line.Substring(0, 3),
                elementSelector: line => (line.Substring(7, 3), line.Substring(12, 3)));
    }

    protected override long PartOne() => Solve("AAA", "ZZZ");

    protected override long PartTwo() => _map.Keys
        .Where(key => key.EndsWith('A'))
        .Select(location => Solve(location, "Z"))
        .Aggregate(1L, LowestCommonMultiple);

    private long Solve(string start, string end)
    {
        var step = 0;
        var location = start;

        while (!location.EndsWith(end)) location = _directions[step++ % _directions.Length] switch
        {
            'L' => _map[location].Left,
            'R' => _map[location].Right,
            _ => throw new NotSupportedException()
        };

        return step;
    }
}
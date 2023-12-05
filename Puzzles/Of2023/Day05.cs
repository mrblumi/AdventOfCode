namespace AdventOfCode.Puzzles.Of2023;

[Puzzle(2023, 05, "If You Give A Seed A Fertilizer")]
public class Day05 : Puzzle<long>
{
    protected override long PartOne() => Solve(seeds => seeds.Select(seed => new Range(seed, 1L)));
    protected override long PartTwo() => Solve(seeds => seeds.Batch(2).Select(range => new Range(range[0], range[1])));

    private long Solve(Func<IEnumerable<long>, IEnumerable<Range>> ranges)
    {
        var almanac = InputLines
            .Split(string.Empty)
            .ToArray();
        
        var rangeNumbers = almanac[0][0]
            .Split(": ")[1]
            .Split(" ")
            .Select(long.Parse);

        return almanac[1..]
            .Aggregate(ranges.Invoke(rangeNumbers), Map)
            .Select(range => range.Start)
            .Min();
    }

    private static IEnumerable<Range> Map(IEnumerable<Range> source, string[] almanac) => Map(
        source: source,
        almanac: almanac[1..]
            .Select(range => range
                .Split(' ')
                .Select(long.Parse)
                .ToArray())
            .ToDictionary(map => new Range(map[1], map[2]), map => map[0] - map[1], new RangeEqualityComparer()));
    
    private static IEnumerable<Range> Map(IEnumerable<Range> source, IDictionary<Range, long> almanac) =>
        Split(source, almanac.Keys.ToArray())
            .Select(range => almanac.TryGetValue(range, out var map)
                ? range with { Start = range.Start + map }
                : range);
    
    private static Range[] Split(IEnumerable<Range> first, Range[] second) =>
        Split(new Queue<Range>(first), boundaries: second).ToArray();

    private static IEnumerable<Range> Split(Queue<Range> queue, Range[] boundaries)
    {
        while (queue.TryDequeue(out var range))
        {
            var processed = false;
            
            foreach (var boundary in boundaries)
            {
                var (result, remaining) = Split(range, boundary);
                
                if (remaining is not null) queue.Enqueue(remaining.Value);
                if (result is null) continue;
                yield return result.Value;

                processed = true;
                break;
            }

            if (!processed) yield return range;
        }
    }

    private static (Range? result, Range? remaining) Split(Range range, Range boundary) =>
        (boundary.Contains(range.Start), boundary.Contains(range.End)) switch
        {
            (true, false) => (range with { Length = boundary.End - range.Start + 1 },
                new Range(boundary.End + 1, range.End - boundary.End)),
            (false, true) => (boundary with { Length = range.End - boundary.Start + 1 },
                range with { Length = boundary.Start - range.Start }),
            (true, true) => (range, null),
            _ => (null, null)
        };
    
    private readonly record struct Range(long Start, long Length)
    {
        public long End => Start + Length - 1;
        
        public bool Contains(Range other) => Start <= other.Start && other.End <= End;
        public bool Contains(long other) => Start <= other && other <= End;
    }
    
    private sealed class RangeEqualityComparer : IEqualityComparer<Range>
    {
        public bool Equals(Range x, Range y) => x.Contains(y) || y.Contains(x);
        public int GetHashCode(Range obj) => 0;
    }
}
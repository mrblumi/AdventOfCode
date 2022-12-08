namespace AdventOfCode.Puzzles.Of2015;

[Puzzle(2015, 02, "I Was Told There Would Be No Math")]
public sealed class Day02 : Puzzle<int>
{
    private readonly IEnumerable<Present> _presents;

    public Day02() => _presents = InputLines
        .Select(_ => new Present(_))
        .ToArray();

    protected override int PartOne() => _presents.Sum(_ => _.RequiredPaper);
    protected override int PartTwo() => _presents.Sum(_ => _.RequiredRibbon);

    private sealed class Present
    {
        private readonly int[] _sides;
        private readonly int[] _surfaces;
        
        public Present(string dimensions)
        {
            var parts = dimensions.Split('x');
            var length = Parse(parts[0]);
            var width = Parse(parts[1]);
            var height = Parse(parts[2]);

            _sides = new[] { length, width, height };
            _surfaces = new[] { length * width, length * height, width * length };
        }
        
        public int RequiredPaper => _surfaces.Min() + 2 * _surfaces.Sum();

        public int RequiredRibbon =>
            2 * _sides.OrderBy(_ => _).Take(2).Sum()
            + _sides.Aggregate(seed: 1, (res, current) => res * current);
    }
}
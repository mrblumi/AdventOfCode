namespace AdventOfCode.Puzzles.Of2015;

public sealed record Day02 : Puzzle
{
    private readonly IEnumerable<Present> _presents;

    public Day02() : base(Year: 2015, Day: 02, "I Was Told There Would Be No Math") =>
        _presents = InputLines
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
            var length = int.Parse(parts[0]);
            var width = int.Parse(parts[1]);
            var height = int.Parse(parts[2]);

            _sides = new[] { length, width, height };
            _surfaces = new[] { length * width, length * height, width * length };
        }
        
        public int RequiredPaper => _surfaces.Min() + 2 * _surfaces.Sum();

        public int RequiredRibbon =>
            2 * _sides.OrderBy(_ => _).Take(2).Sum()
            + _sides.Aggregate(seed: 1, (res, current) => res * current);
    }
    
    private sealed class NotEqualComparer : IEqualityComparer<int>
    {
        public static NotEqualComparer Instance = new();
        
        public bool Equals(int x, int y) => x != y;
        public int GetHashCode(int obj) => 0;
    }
}
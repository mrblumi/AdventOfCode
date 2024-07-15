using System.Text;

namespace AdventOfCode.Puzzles.Of2015;

[Puzzle(2015, 18, "Like a GIF For Your Yard")]
public class Day18 : Puzzle<int>
{
    protected override int PartOne() => Solve();
    protected override int PartTwo() => Solve(withStuckEdges: true);

    private int Solve(bool withStuckEdges = false)
    {
        var grid = Grid.Init(InputLines, withStuckEdges);
        for (var step = 0; step < 100; step++) grid = grid.Update();

        return grid.Cells.Cast<bool>().Count(_ => _);
    }

    private sealed record Grid(int Width, int Height, bool[,] Cells, Func<int, int, bool>? IsStuck = null)
    {
        public static Grid Init(string[] source, bool hasStuckEdges = false)
        {
            var width = source.Select(_ => _.Length).Distinct().Single();
            var height = source.Length;

            var isStuck = hasStuckEdges
                ? new Func<int, int, bool>((x, y) =>
                    x == 0 && y == 0 ||
                    x == width - 1 && y == 0 ||
                    x == 0 && y == height - 1 ||
                    x == width - 1 && y == height - 1)
                : null;
            
            var cells = new bool[width, height];
            
            for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
                if (isStuck?.Invoke(x, y) is true || source[y][x] is '#')
                    cells[x,y] = true;

            return new(width, height, cells, isStuck);
        }

        public Grid Update()
        {
            var cells = new bool[Width, Height];

            for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
                cells[x, y] = IsStuck?.Invoke(x, y) is true || CountShiningNeighboursOf(x, y) switch
                {
                    2 => Cells[x, y],
                    3 => true,
                    _ => false
                };
            
            return this with { Cells = cells };
        }
        
        private int CountShiningNeighboursOf(int x1, int y1)
        {
            var count = 0;
            
            for (var x2 = -1; x2 <= 1; x2++)
            for (var y2 = -1; y2 <= 1; y2++)
                if (x2 != 0 || y2 != 0)
                if (0 <= x1 + x2 && x1 + x2 < Width)
                if (0 <= y1 + y2 && y1 + y2 < Height)
                if (Cells[x1 + x2, y1 + y2])
                    count++;

            return count;
        }
    }
}
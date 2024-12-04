namespace AdventOfCode.Puzzles.Of2024;

[Puzzle(2024, 04, "Ceres Search")]
public class Day04 : Puzzle<int>
{
    private readonly int _length;
    private readonly int _height;
    
    public Day04()
    {
        _length = InputLines[0].Length;
        _height = InputLines.Length;
    }

    protected override int PartOne()
    {
        var directions = new[] { (1, 0), (1, 1), (0, 1), (-1, 1), (-1, 0), (-1, -1), (0, -1), (1, -1) };
        var count = 0;
        
        for (var y = 0; y < _height; y++)
        for (var x = 0; x < _height; x++)
            foreach (var direction in directions)
                if (Hit(new(x, y), direction, "XMAS"))
                    count++;

        return count;
    }

    protected override int PartTwo()
    {
        var patterns = new[] { "MAS", "SAM" };
        var count = 0;
        
        for (var y = 0; y < _height; y++)
        for (var x = 0; x < _height; x++)
            if (Hit(new(x, y), (1, 1), patterns))
                if (Hit(new(x, y + 2), (1, -1), patterns))
                    count++;

        return count;
    }

    private bool Hit(Coordinate position, (int, int) direction, string[] patterns) =>
        patterns.Any(pattern => Hit(position, direction, pattern));
    
    private bool Hit(Coordinate position, (int, int) direction, string pattern)
    {
        foreach (var character in pattern)
        {
            if (position.X < 0 || _length <= position.X || position.Y < 0 || _height <= position.Y) return false;
            if (character != InputLines[position.Y][position.X]) return false;

            position += direction;
        }

        return true;
    }
}
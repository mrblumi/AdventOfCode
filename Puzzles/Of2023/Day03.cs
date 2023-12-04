namespace AdventOfCode.Puzzles.Of2023;

[Puzzle(2023, 03, "Gear Ratios")]
public class Day03 : Puzzle<int>
{
    private delegate int GetValue(int y, ref int x);
    
    private readonly int _gridLength;
    private readonly int _gridHeight;

    public Day03()
    {
        _gridLength = InputLines.Select(_ => _.Length).Distinct().Single();
        _gridHeight = InputLines.Length;
    }

    protected override int PartOne() =>
        Solve((int y, ref int x) =>
            GetNumber(y, ref x, out var number) &&
            Neighbours(new(x - 1, y), number.Length).Any(c => !"0.123456789".Contains(InputLines[c.Y][c.X]))
                ? Parse(number)
                : 0);
    
    protected override int PartTwo() =>
        Solve((int y, ref int x) =>
            InputLines[y][x] is '*' &&
            GetNumbers(Neighbours(new(x, y)), out var numbers)
                ? numbers[0] * numbers[1]
                : 0);

    private int Solve(GetValue getValue)
    {
        int sum = 0;
        
        for (int y = 0; y < _gridLength; y++)
        for (int x = 0; x < _gridHeight; x++)
            sum += getValue(y, ref x);

        return sum;
    }
    
    private bool GetNumber(int y, ref int x, out string number, HashSet<Coordinate>? visited = null)
    {
        number = string.Empty;
        
        while (x < _gridLength && char.IsDigit(InputLines[y][x]))
        {
            visited?.Add(new Coordinate(x, y));
            
            number += InputLines[y][x];
            x++;
        }

        return number != string.Empty;
    }
    
    private bool GetNumbers(IEnumerable<Coordinate> coordinates, out IList<int> result)
    {
        result = new List<int>();
        
        var visited = new HashSet<Coordinate>();
        foreach (var coordinate in coordinates)
            if (visited.Add(coordinate))
            {
                var x = coordinate.X;
                var shifted = false;

                while (x >= 0 && char.IsDigit(InputLines[coordinate.Y][x])) shifted = BackShift(ref x);
                
                if (shifted) x++;
                if (GetNumber(coordinate.Y, ref x, out var number, visited)) result.Add(Parse(number));
            }
        
        return result.Count == 2;
    }
    
    private IEnumerable<Coordinate> Neighbours(Coordinate coordinate, int length = 1)
    {
        for (var j = coordinate.Y - 1; j <= coordinate.Y + 1; j++)
        for (var i = coordinate.X - length; i <= coordinate.X + 1; i ++)
            if (0 <= i && i < _gridHeight && 0 <= j && j < _gridLength)
                yield return new Coordinate(i, j);
    }

    private static bool BackShift(ref int x)
    {
        x--;
        return true;
    }
}
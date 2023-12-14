namespace AdventOfCode.Puzzles.Of2023;

[Puzzle(2023, 09, "Mirage Maintenance")]
public class Day09 : Puzzle<int>
{
    private readonly IList<List<List<int>>> _data;

    public Day09()
    {
        static IEnumerable<List<int>> SpanPattern(List<int> line)
        {
            yield return line;
            while (line.Any(number => number != 0)) yield return line = line[1..].Zip(line, (x, y) => x - y).ToList();
        }
        
        _data = InputLines
            .Select(line => line.Split(' ').Select(Parse).ToList())
            .Select(line => SpanPattern(line).ToList())
            .ToArray();
    }

    protected override int PartOne() => Solve((x, y) => x + y, ^1);
    protected override int PartTwo() => Solve((x, y) => x - y, 0);

    private int Solve(Func<int, int, int> operation, Index index)
    {
        int Extrapolate(List<List<int>> matrix)
        {
            var result = 0;
            for (int i = matrix.Count - 1; i >= 0; i--)
            {
                result = operation.Invoke(matrix[i][index], result);
            }

            return result;
        }
        
        return _data.Select(Extrapolate).Sum();
    }
}
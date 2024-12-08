namespace AdventOfCode.Puzzles.Of2024;

[Puzzle(2024, 07, "Bridge Repair")]
public class Day07 : Puzzle<long>
{
    private readonly long[][] _numbers;

    public Day07() => _numbers = InputLines
        .Select(x => x.Split([':', ' '], StringSplitOptions.RemoveEmptyEntries))
        .Select(x => x.Select(long.Parse))
        .Select(x => x.ToArray())
        .ToArray();

    protected override long PartOne() => Solve(Addition, Multiplication);
    protected override long PartTwo() => Solve(Addition, Multiplication, Concatenation);

    private long Solve(params Func<long, long, long>[] operators) => _numbers
        .Where(x => CanBeTrue(x[0], x[1..], operators))
        .Sum(x => x[0]);

    private static bool CanBeTrue(long value, long[] numbers, Func<long, long, long>[] operators) => numbers switch
    {
        [] => false,
        [var x] => value == x,
        [var x, var y, .. var remaining] => operators.Any(op => CanBeTrue(value, [op(x, y), ..remaining], operators)),
    };

    private static long Addition(long a, long b) => a + b;
    private static long Multiplication(long a, long b) => a * b;
    private static long Concatenation(long a, long b) => long.Parse($"{a}{b}");
}
using System.Diagnostics;

namespace AdventOfCode.Commons;

public abstract record Puzzle(int Year, int Day, string Description)
{
    private string InputFile => $"Puzzles/Of{Year}/Day{Day:00}.input";

    private string? _inputText;
    protected string InputText => _inputText ??= File.ReadAllText(InputFile);

    private string[]? _inputLines;
    protected string[] InputLines => _inputLines ??= File.ReadAllLines(InputFile);

    protected abstract int PartOne();
    protected abstract int PartTwo();

    public void Solve()
    {
        Console.WriteLine($"--- {Year}, day {Day}: {Description} ---");
        Console.WriteLine($"Part one: {PartOne()}");
        Console.WriteLine($"Part two: {PartTwo()}");
    }
}
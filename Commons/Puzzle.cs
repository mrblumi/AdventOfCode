using System.Reflection;

namespace AdventOfCode.Commons;

public abstract class Puzzle<TResult> : IPuzzle
{
    private readonly PuzzleAttribute _attribute;
    private readonly string _inputFile;

    protected Puzzle()
    {
        _attribute = GetType().GetCustomAttributes<PuzzleAttribute>().Single();
        _inputFile = $"Puzzles/Of{_attribute.Year}/Day{_attribute.Day:00}.input";
    }

    private string? _inputText;
    protected string InputText => _inputText ??= File.ReadAllText(_inputFile);

    private string[]? _inputLines;
    protected string[] InputLines => _inputLines ??= File.ReadAllLines(_inputFile);

    protected abstract TResult PartOne();
    protected abstract TResult PartTwo();

    public void Solve()
    {
        Console.WriteLine($"--- {_attribute.Year}, day {_attribute.Day}: {_attribute.Description} ---");
        Console.WriteLine($"Part one: {PartOne()}");
        Console.WriteLine($"Part two: {PartTwo()}");
    }
}
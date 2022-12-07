namespace AdventOfCode.Commons;

public abstract record Puzzle(int Year, int Day, string Description)
{
    private readonly string _inputFile = $"Puzzles/Of{Year}/Day{Day:00}.input";

    private string? _inputText;
    protected string InputText => _inputText ??= File.ReadAllText(_inputFile);

    private string[]? _inputLines;
    protected string[] InputLines => _inputLines ??= File.ReadAllLines(_inputFile);

    protected abstract object PartOne();
    protected abstract object PartTwo();

    public void Solve()
    {
        Console.WriteLine($"--- {Year}, day {Day}: {Description} ---");
        Console.WriteLine($"Part one: {PartOne()}");
        Console.WriteLine($"Part two: {PartTwo()}");
    }
}
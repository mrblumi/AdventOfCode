namespace AdventOfCode.Puzzles.Of2022;

[Puzzle(2022, 05, "Supply Stacks")]
public sealed class Day05 : Puzzle<string>
{
    private sealed record Instruction(int Count, int Source, int Target);

    private readonly Instruction[] _instructions;
    private readonly string[] _setup;

    public Day05()
    {
        var separator = Array.IndexOf(InputLines, string.Empty);

        _instructions = InputLines[(separator + 1)..]
            .Select(_ => _.Split(" "))
            .Select(_ => new Instruction(Count: Parse(_[1]), Source: Parse(_[3]) - 1, Target: Parse(_[5]) - 1))
            .ToArray();
        _setup = InputLines[..separator];
    }

    private Stack<char>[] Setup()
    {
        var stacks = _setup[^1].Chunk(4).Select(_ => new Stack<char>()).ToArray();
        foreach (var line in _setup[..^1].Reverse())
        foreach (var (stack, @char) in stacks.Zip(line.Chunk(4).Select(_ => _[1])))
            if(char.IsLetter(@char)) stack.Push(@char);
        return stacks;
    }
    
    private string Solve(Func<IEnumerable<char>, IEnumerable<char>>? orientation = null)
    {
        var stacks = Setup();

        foreach (var instruction in _instructions)
        {
            var source = stacks[instruction.Source];
            var target = stacks[instruction.Target];
            var items = Enumerable.Range(0, instruction.Count).Select(_ => source.Pop());

            if (orientation != null) items = orientation.Invoke(items);
            items.ForEach(target.Push);
        }
        
        return new string(stacks.Select(_ => _.Pop()).ToArray());
    }

    protected override string PartOne() => Solve();
    protected override string PartTwo() => Solve(Enumerable.Reverse);
}
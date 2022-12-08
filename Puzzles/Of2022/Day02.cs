namespace AdventOfCode.Puzzles.Of2022;

[Puzzle(2022, 02, "Rock Paper Scissors")]
public sealed class Day02 : Puzzle<int>
{
    private static int Score((int Player, int Opponent) _) => _.Player + 1 + _ switch
    {
        var (x,y) when (x - y + 3) % 3 == 1 => 6,
        var (x,y) when x == y => 3,
        _ => 0
    };
    
    private static int ParseOption(char option) => option switch
    {
        'A' or 'X' => 1,
        'B' or 'Y' => 2,
        'C' or 'Z' => 3,
        _ => throw new NotSupportedException()
    };

    private static Func<int, int> ParseStrategy(char strategy) => option => strategy switch
    {
        'X' => (option + 2) % 3,
        'Y' => option,
        'Z' => (option + 1) % 3,
        _ => throw new NotSupportedException()
    };

    protected override int PartOne() => InputLines
        .Select(_ => (Player: ParseOption(_[2]), Opponent: ParseOption(_[0])))
        .Sum(Score);

    protected override int PartTwo() => InputLines
        .Select(_ => (Instruction: _[2], Opponent: ParseOption(_[0])))
        .Select(_ => (Player: ParseStrategy(_.Instruction).Invoke(_.Opponent), _.Opponent))
        .Sum(Score);
}
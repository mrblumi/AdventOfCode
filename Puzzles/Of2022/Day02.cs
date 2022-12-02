namespace AdventOfCode.Puzzles.Of2022;

public sealed record Day02() : Puzzle(Year: 2022, Day: 02, "Rock Paper Scissors")
{
    private enum Option { Rock, Paper, Scissors }

    private static int Score((Option Player, Option Opponent) _) => (int)_.Player + 1 + _ switch
    {
        var (x,y) when (x - y + 3) % 3 == 1 => 6,
        var (x,y) when x == y => 3,
        _ => 0
    };
    
    private static Option ParseOption(char option) => option switch
    {
        'A' or 'X' => Option.Rock,
        'B' or 'Y' => Option.Paper,
        'C' or 'Z' => Option.Scissors,
        _ => throw new NotSupportedException()
    };

    private static Func<Option, Option> ParseStrategy(char strategy) => option => strategy switch
    {
        'X' => (Option)((int)(option + 2) % 3),
        'Y' => option,
        'Z' => (Option)((int)(option + 1) % 3),
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
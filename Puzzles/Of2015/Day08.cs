namespace AdventOfCode.Puzzles.Of2015;

public sealed record Day08() : Puzzle(Year: 2015, Day: 08, "Matchsticks")
{
    protected override object PartOne()
    {
        int DecodedLength(ReadOnlySpan<char> input) => input switch
        {
            ['\\', 'x', _, _, .. var rest] => 1 + DecodedLength(rest),
            ['\\', _, .. var rest] => 1 + DecodedLength(rest),
            [ '"', .. var rest ] => 0 + DecodedLength(rest),
            [ _, .. var rest ] => 1 + DecodedLength(rest),
            _ => 0
        };

        return InputLines
            .Select(_ => _.Length - DecodedLength(_.AsSpan()))
            .Sum();
    }

    protected override object PartTwo()
    {
        int EncodedLength(string input) => input.Length + input.Count("\"\\".Contains) + 2;

        return InputLines
            .Select(_ => EncodedLength(_) - _.Length)
            .Sum();
    }
}
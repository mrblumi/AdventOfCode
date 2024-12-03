using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzles.Of2024;

[Puzzle(2024, 03, "Mull It Over")]
public partial class Day03 : Puzzle<int>
{
    private readonly int _partOne;
    private readonly int _partTwo;

    [GeneratedRegex(@"(don't\(\)|do\(\)|mul\((?<x>\d+),(?<y>\d+)\))")]
    private partial Regex Regex { get; }
    
    public Day03()
    {
        var enabled = true;

        foreach (var match in InputLines.SelectMany(x => Regex.Matches(x)))
        {
            if (match.Value is "do()") enabled = true;
            if (match.Value is "don't()") enabled = false;
            if (match.Value[..3] is not "mul") continue;
            
            var x = Parse(match.Groups["x"].Value);
            var y = Parse(match.Groups["y"].Value);
            
            _partOne += x * y;
            if (enabled) _partTwo += x * y;
        }
    }

    protected override int PartOne() => _partOne;
    protected override int PartTwo() => _partTwo;
}
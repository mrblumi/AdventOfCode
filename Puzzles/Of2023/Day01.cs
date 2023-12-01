using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzles.Of2023;

[Puzzle(2023, 01, "Trebuchet?!")]
public class Day01 : Puzzle<int>
{
    private static int Parse(string input) => input switch
    {
        "1" or "one" => 1,
        "2" or "two" => 2,
        "3" or "three" => 3,
        "4" or "four" => 4,
        "5" or "five" => 5,
        "6" or "six" => 6,
        "7" or "seven" => 7,
        "8" or "eight" => 8,
        "9" or "nine" => 9,
        _ => throw new ArgumentOutOfRangeException(input)
    };
    
    private static Func<string, int> CalibrationValue(string pattern) => input =>
    {
        var first = Regex.Match(input, $"(?<digit>{pattern})").Groups["digit"].Value;
        var last = Regex.Match(input, $"(?<digit>{pattern})", RegexOptions.RightToLeft).Groups["digit"].Value;

        return Parse(first) * 10 + Parse(last);
    };

    protected override int PartOne()
        => InputLines.Select(CalibrationValue(@"\d")).Sum();

    protected override int PartTwo()
        => InputLines.Select(CalibrationValue(@"one|two|three|four|five|six|seven|eight|nine|\d")).Sum();
}
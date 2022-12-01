namespace AdventOfCode.Puzzles.Of2015;

public sealed record Day05() : Puzzle(Year: 2015, Day: 05, "Doesn't He Have Intern-Elves For This?")
{
    protected override int PartOne() => InputLines.Count(IsNice(EnoughVowels, NoInvalid, AnyTwiceInARow));
    protected override int PartTwo() => InputLines.Count(IsNice(AnyPairTwiceInARow, AnyRepeatsWithAnotherBetween));

    private static Func<string, bool> IsNice(params Func<string, bool>[] conditions) =>
        input => conditions.All(condition => condition.Invoke(input));

    private static readonly char[] Vowels = { 'a', 'e', 'i', 'o', 'u' };
    private static bool EnoughVowels(string input) => input.Count(Vowels.Contains) >= 3;

    private static readonly string[] Invalid = { "ab", "cd", "pq", "xy" };
    private static bool NoInvalid(string input) => !Invalid.Any(input.Contains);
    
    private static bool AnyTwiceInARow(string input)
    {
        char? last = null;
        foreach (var character in input)
        {
            if (last == character) return true;
            last = character;
        }

        return false;
    }

    private static bool AnyPairTwiceInARow(string input) => Enumerable
        .Range(0, input.Length - 1)
        .Any(index => input.IndexOf(input.Substring(index, 2), index + 2, StringComparison.InvariantCulture) >= 0);

    private static bool AnyRepeatsWithAnotherBetween(string input) => Enumerable
        .Range(0, input.Length - 2)
        .Any(i => input[i] == input[i + 2]);
}
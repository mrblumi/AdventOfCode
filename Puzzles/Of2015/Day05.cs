namespace AdventOfCode.Puzzles.Of2015;

using static Enumerable;

public sealed record Day05() : Puzzle(Year: 2015, Day: 05, "Doesn't He Have Intern-Elves For This?")
{
    private static readonly char[] Vowels = { 'a', 'e', 'i', 'o', 'u' };
    private static readonly string[] Invalid = { "ab", "cd", "pq", "xy" };

    protected override object PartOne() => InputLines.Count(
        input =>
            input.Count(Vowels.Contains) >= 3 && // has at least three vowels
            !Invalid.Any(input.Contains) && // does not contain invalid substrings
            Range(0, input.Length - 1).Any(i => input[i] == input[i + 1]) // one char is repeated
        );  
    
    protected override object PartTwo() => InputLines.Count(
        input =>
            Range(0, input.Length - 2).Any(i => input[i] == input[i + 2]) && // one char repeats with another between
            Range(0, input.Length - 1).Any(i => input.IndexOf(input.Substring(i, 2), i + 2) >= 0) // one pair repeats
        );
}
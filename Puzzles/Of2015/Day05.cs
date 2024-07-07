namespace AdventOfCode.Puzzles.Of2015;

[Puzzle(2015, 05, "Doesn't He Have Intern-Elves For This?")]
public sealed class Day05 : Puzzle<int>
{
    private static readonly char[] Vowels = ['a', 'e', 'i', 'o', 'u'];
    private static readonly string[] Invalid = ["ab", "cd", "pq", "xy"];

    protected override int PartOne() => InputLines.Count(
        input =>
            input.Count(Vowels.Contains) >= 3 && // has at least three vowels
            !Invalid.Any(input.Contains) && // does not contain invalid substrings
            Enumerable.Range(0, input.Length - 1).Any(i => input[i] == input[i + 1]) // one char is repeated
        );  
    
    protected override int PartTwo() => InputLines.Count(
        input =>
            Enumerable.Range(0, input.Length - 2).Any(i => input[i] == input[i + 2]) && // one repeats, one between
            Enumerable.Range(0, input.Length - 1).Any(i => input.IndexOf(input.Substring(i, 2), i + 2, StringComparison.Ordinal) >= 0) // pair
        );
}
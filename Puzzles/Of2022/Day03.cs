namespace AdventOfCode.Puzzles.Of2022;

[Puzzle(2022, 03, "Rucksack Reorganization")]
public sealed class Day03 : Puzzle<int>
{
    private static IEnumerable<char>[] Chunks(string s) =>
        s.Chunk(s.Length / 2).Cast<IEnumerable<char>>().ToArray();

    private static char Intersection(IEnumerable<char>[] chunks) =>
        chunks[1..].Aggregate(chunks[0], (result, current) => result.Intersect(current)).Single();

    private static int Priority(char c) =>
        c switch { <= 'z' and >= 'a' => c - 'a' + 1, <= 'Z' and >= 'A' => c - 'A' + 27, _ => 0 };

    protected override int PartOne() =>
        InputLines.Select(Chunks).Select(Intersection).Sum(Priority);

    protected override int PartTwo() =>
        InputLines.Cast<IEnumerable<char>>().Chunk(3).Select(Intersection).Sum(Priority);
}
using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzles.Of2015;

[Puzzle(2015, 16, "Aunt Sue")]
public class Day16 : Puzzle<int>
{
    private readonly IReadOnlyCollection<AuntSue> _auntSues;
    private readonly IReadOnlyDictionary<string, int> _factsAboutSue;

    public Day16()
    {
        _auntSues = InputLines.Select(AuntSue.Parse).ToArray();
        _factsAboutSue = new Dictionary<string, int>
        {
            ["children"] = 3,
            ["cats"] = 7,
            ["samoyeds"] = 2,
            ["pomeranians"] = 3,
            ["akitas"] = 0,
            ["vizslas"] = 0,
            ["goldfish"] = 5,
            ["trees"] = 3,
            ["cars"] = 2,
            ["perfumes"] = 1
        };
    }

    protected override int PartOne() => Solve((things, amount) => AuntSueMightHave(TheExact, amount, of: things));

    protected override int PartTwo() => Solve((things, amount) => things switch
    {
        "cats" or "trees" => AuntSueMightHave(MoreThan, amount, of: things),
        "goldfish" or "pomeranians" => AuntSueMightHave(LessThan, amount, of: things),
        _ => AuntSueMightHave(TheExact, amount, of: things),
    });

    private int Solve(Func<string, int, Func<AuntSue, bool>> condition) => _factsAboutSue
        .Aggregate(
            seed: _auntSues.AsEnumerable(),
            func: (auntSues, x) => auntSues.Where(condition(x.Key, x.Value)))
        .Single().Number;
    
    private static Func<AuntSue, bool> AuntSueMightHave(Func<int, int, bool> compare, int amount, string of) =>
        auntSue => auntSue.ContainsKey(of) is false || compare(auntSue[of], amount);
    
    private class AuntSue(IDictionary<string, int> compounds) : Dictionary<string, int>(compounds)
    {
        public int Number { get; init; }

        public static AuntSue Parse(string input)
        {
            var matches = Regex.Match(input);
            var compounds = matches.Groups["compound"].Captures
                .Select(x => x.Value.Split(": "))
                .ToDictionary(x => x[0], x => int.Parse(x[1]));

            return new(compounds) { Number = int.Parse(matches.Groups["sue"].Value) };
        }

        private static Regex Regex = new(@"Sue (?<sue>\d+): ((?<compound>\w+: \d+)[, ]{0,2})*");
    }
    
    private static bool TheExact(int i, int j) => i == j;
    private static bool LessThan(int i, int j) => i < j;
    private static bool MoreThan(int i, int j) => i > j;
}
using System.Collections.Immutable;

namespace AdventOfCode.Puzzles.Of2015;

[Puzzle(2015, 13, "Knights of the Dinner Table")]
public class Day13 : Puzzle<int>
{
    private readonly ImmutableDictionary<string, ImmutableDictionary<string, int>> _settings;

    public Day13() => _settings = InputLines
        .Select(line => line.Trim('.').Split(' '))
        .Select(Setting.Parse)
        .GroupBy(setting => setting.Person)
        .ToImmutableDictionary(
            group => group.Key,
            group => group
                .ToImmutableDictionary(
                    setting => setting.Neighbour,
                    setting => setting.Happiness));
    
    private int Solve(ImmutableDictionary<string, ImmutableDictionary<string, int>> settings) => settings.Keys
        .ToArray()
        .Permutations()
        .Select(people => people
            .Select((person, i) =>
                settings[person][people[(i + 1) % people.Length]] +
                settings[person][people[(i + people.Length - 1) % people.Length]])
            .Sum())
        .Max();

    protected override int PartOne() => Solve(_settings);

    protected override int PartTwo()
    {
        var settings = _settings
            .Select(pair => new { pair.Key, Value = pair.Value.Add("me", 0) })
            .Append(new { Key = "me", Value = _settings.Keys.ToImmutableDictionary(person => person, _ => 0) })
            .ToImmutableDictionary(pair => pair.Key, pair => pair.Value);

        return Solve(settings);
    }

    private sealed record Setting(string Person, string Neighbour, int Happiness)
    {
        public static Setting Parse(string[] parts) => new(
            Person: parts[0],
            Neighbour: parts[10],
            Happiness: parts[2] is "gain" ? int.Parse(parts[3]) : -int.Parse(parts[3]));
    }
}
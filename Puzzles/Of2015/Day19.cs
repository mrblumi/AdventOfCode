using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzles.Of2015;

[Puzzle(2015, 19, "Medicine for Rudolph")]
public class Day19 : Puzzle<int>
{
    private readonly string _molecule;
    
    public Day19() => _molecule = InputLines[^1];

    protected override int PartOne()
    {
        var reactions = InputLines[..^2]
            .Select(_ => _.Split(" => "))
            .Select(_ => new Reaction(_[0], _[1]))
            .ToArray();

        return new NuclearFusionPlant(reactions)
            .Invoke(_molecule)
            .Distinct()
            .Count();
    }

    protected override int PartTwo()
    {
        var analysis = new Regex("((?<atom>[A-Z][a-df-z]?|e))*");
        var atoms = analysis
            .Match(_molecule)
            .Groups["atom"]
            .Captures
            .Select(_ => _.Value)
            .ToArray();
        
        var result = atoms.Length;

        foreach (var atom in atoms)
        {
            if (atom is "Rn" or "Ar") result -= 1;
            if (atom is "Y") result -= 2;
        }

        return result - 1;
    }

    private readonly record struct Reaction(string From, string To);
    
    private sealed class NuclearFusionPlant(IReadOnlyCollection<Reaction> reactions)
    {
        public IEnumerable<string> Invoke(string sourceMolecule)
        {
            foreach (var reaction in reactions)
            {
                var startIndex = 0;
            
                while (true)
                {
                    var index = sourceMolecule.IndexOf(reaction.From, startIndex, StringComparison.Ordinal);
                    if (index < 0) break;

                    yield return sourceMolecule[..index] + reaction.To + sourceMolecule[(index + reaction.From.Length)..];
                    startIndex = index + reaction.From.Length;
                }
            }
        }
    }
}
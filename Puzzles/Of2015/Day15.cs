using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzles.Of2015;

[Puzzle(2015, 15, "Science for Hungry People")]
public partial class Day15 : Puzzle<int>
{
    private readonly Ingredients _ingredients;
    
    public Day15() => _ingredients = new(InputLines);

    protected override int PartOne() => Solve();
    protected override int PartTwo() => Solve(calories: 500);

    private int Solve(int? calories = null)
    {
        var enumerable = BinomialDistribution(100, _ingredients.List.Count)
            .Select(x => new Cookie(_ingredients, x))
            .Select(x => x.Score)
            .Where(x => x.All(y => y > 0));

        if (calories.HasValue) enumerable = enumerable.Where(x => x[^1] == calories.Value);
        
        return enumerable
            .Select(x => x[..^1].Aggregate(1, (current, property) => current * property))
            .Max();
    }

    private static IEnumerable<int[]> BinomialDistribution(int n, int k)
    {
        if (k is 1) yield return [n];
        else for (var i = 0; i < n; i++)
            foreach (var distribution in BinomialDistribution(n - i, k - 1))
                yield return [..distribution, i];
    }

    private sealed partial class Ingredients(IEnumerable<string> lines)
    {
        public IReadOnlyCollection<int[]> List { get; } = lines.Select(Parse).ToArray();
        public int Properties => List.Select(x => x.Length).Distinct().Single();
        
        private static int[] Parse(string line) => Regex
            .Match(line).Groups
            .Cast<Group>()
            .Skip(1)
            .Select(x => x.Value)
            .Select(int.Parse)
            .ToArray();

        [GeneratedRegex("^.+: capacity (.+), durability (.+), flavor (.+), texture (.+), calories (.+)$")]
        private static partial Regex Regex { get; }
    }

    private sealed class Cookie(Ingredients ingredients, int[] amounts)
    {
        private readonly (int[] Ingredient, int Amount)[] _mixture = ingredients.List.Zip(amounts).ToArray();
        
        public int[] Score => Enumerable.Range(0, ingredients.Properties).Select(PropertyScore).ToArray();

        private int PropertyScore(int i) => _mixture.Select(y => y.Amount * y.Ingredient[i]).Sum();
    }
}
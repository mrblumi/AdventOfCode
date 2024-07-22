namespace AdventOfCode.Puzzles.Of2015;

[Puzzle(2015, 20, "Infinite Elves and Infinite Houses")]
public class Day20 : Puzzle<int>
{
    protected override int PartOne() => Solve(packages: 10);
    protected override int PartTwo() => Solve(packages: 11, visits: 50);

    private int Solve(int packages, int? visits = null)
    {
        Func<int, IEnumerable<int>> divisors = visits is null
            ? i => Divisors(i)
            : i => Divisors(i).Where(x => i / x <= visits);
        
        var house = 1;
        while (packages * divisors.Invoke(house).Sum() <= 29000000)
            house++;

        return house;
    }
    
    private static IEnumerable<int> Divisors(int input)
    {
        var primeFactors = input
            .PrimeFactorization()
            .Where(x => x != input)
            .ToArray();
        
        return primeFactors
            .PowerSet()
            .Select(Product)
            .Append(input)
            .Distinct();
    }

    private static int Product(IEnumerable<int> numbers) => numbers.Aggregate(1, (x, y) => x * y);
}
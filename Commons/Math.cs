using System.Numerics;

namespace AdventOfCode.Commons;

public static class Math
{
    public static TNumber LowestCommonMultiple<TNumber>(TNumber a, TNumber b)
        where TNumber : INumber<TNumber> =>
        a * b / GreatestCommonDivisor(a, b);
    
    public static TNumber GreatestCommonDivisor<TNumber>(TNumber a, TNumber b)
        where TNumber : INumber<TNumber> =>
        b == TNumber.Zero ? a : GreatestCommonDivisor(b, a % b);

    
    
    private static readonly HashSet<int> PrimeNumbers = [2];

    public static bool IsPrimeNumber(this int i)
    {
        if (i < 2)
            return false;

        foreach (var j in PrimeNumbers)
            if (i % j == 0)
                return false;

        for (var j = PrimeNumbers.Max() + 1; j < (int)System.Math.Ceiling(System.Math.Sqrt(i)); j++)
            if (IsPrimeNumber(j) && i % j == 0)
                return false;

        PrimeNumbers.Add(i);
        return true;
    }
    
    public static IEnumerable<int> PrimeFactorization(this int input)
    {
        var current = 0;
		
        using var enumerator = PrimeNumbers.GetEnumerator();
		
        while (input != 1 && enumerator.MoveNext())
        {
            current = enumerator.Current;
			
            while (input % current == 0)
            {
                input /= current;
                yield return current;
            }
        }
		
        while (input != 1)
        {
            if (current.IsPrimeNumber())
            {
                while (input % current == 0)
                {
                    input /= current;
                    yield return current;
                }
            }
            
            current++;
        }
    }
}
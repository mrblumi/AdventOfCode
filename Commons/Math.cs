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
}
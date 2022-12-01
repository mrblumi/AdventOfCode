using System.Numerics;

namespace AdventOfCode.Commons;

public static class NumberExtensions
{
    public static bool IsBetween<TNumber>(this TNumber self, TNumber start, TNumber end)
        where TNumber : INumber<TNumber> =>
        start <= self && self <= end;
}
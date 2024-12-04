namespace AdventOfCode.Extensions;

public static class ObjectExtensions
{
    public static TOut Then<TIn, TOut>(this TIn item, Func<TIn, TOut> expression) => expression.Invoke(item);
}
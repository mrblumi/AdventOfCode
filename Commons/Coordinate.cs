using System.Numerics;

namespace AdventOfCode.Commons;

public record Coordinate(int X, int Y)
{
    public static Coordinate operator +(Coordinate left, Coordinate right) => new(left.X + right.X, left.Y + right.Y);
    public static Coordinate operator -(Coordinate left, Coordinate right) => new(left.X - right.X, left.Y - right.Y);

    public static Coordinate operator *(int left, Coordinate right) => new(left * right.X, left * right.Y);
    
    public static implicit operator Coordinate((int, int) tuple) => new(tuple.Item1, tuple.Item2);
}
using System.Numerics;

namespace AdventOfCode.Commons;

public record Coordinate(int X, int Y)
    : IAdditionOperators<Coordinate, Coordinate, Coordinate>
    , IAdditionOperators<Coordinate, (int, int), Coordinate>
    , ISubtractionOperators<Coordinate, Coordinate, Coordinate>
    , ISubtractionOperators<Coordinate, (int, int), Coordinate>
{
    public static Coordinate operator +(Coordinate left, Coordinate right) => new(left.X + right.X, left.Y + right.Y);
    public static Coordinate operator +(Coordinate left, (int, int) right) => new(left.X + right.Item1, left.Y + right.Item2);
    
    public static Coordinate operator -(Coordinate left, Coordinate right) => new(left.X - right.X, left.Y - right.Y);
    public static Coordinate operator -(Coordinate left, (int, int) right) => new(left.X - right.Item1, left.Y - right.Item2);
}
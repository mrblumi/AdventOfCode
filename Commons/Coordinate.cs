using System.Numerics;

namespace AdventOfCode.Commons;

public record Coordinate(int X, int Y)
    : IAdditionOperators<Coordinate, Coordinate, Coordinate>
    , ISubtractionOperators<Coordinate, Coordinate, Coordinate>
{
    public static Coordinate operator +(Coordinate left, Coordinate right) => new(left.X + right.X, left.Y + right.Y);
    public static Coordinate operator -(Coordinate left, Coordinate right) => new(left.X - right.X, left.Y - right.Y);
}
using System.Numerics;

namespace AdventOfCode.Puzzles.Of2022;

[Puzzle(2022, 09, "Rope Bridge")]
public sealed class Day09 : Puzzle<int>
{
    private sealed record Knot(int X, int Y)
    {
        public static Knot operator +(Knot left, Knot right) => new(left.X + right.X, left.Y + right.Y);
        public static Knot operator -(Knot left, Knot right) => new(left.X - right.X, left.Y - right.Y);
    }
    
    private int TraceLastKnot(int length)
    {
        var origin = new Knot(0, 0);
        var knots = Enumerable.Range(0, length).Select(_ => origin).ToArray();
        var positions = new HashSet<Knot>(knots);

        foreach (var instruction in InputLines)
        {
            var direction = instruction[0];
            var repetition = Parse(instruction.Substring(2));

            while (repetition > 0)
            {
                knots[0] = direction switch
                {
                    'U' => knots[0] with { X = knots[0].X + 1 },
                    'D' => knots[0] with { X = knots[0].X - 1 },
                    'L' => knots[0] with { Y = knots[0].Y - 1 },
                    'R' => knots[0] with { Y = knots[0].Y + 1 },
                    _ => throw new NotSupportedException($"{nameof(direction)}: {direction}")
                };

                for (var i = 1; i < length; i++)
                {
                    var distance = knots[i - 1] - knots[i];
                    if (Math.Max(Math.Abs(distance.X), Math.Abs(distance.Y)) == 2)
                        knots[i] += new Knot(X: Math.Sign(distance.X), Y: Math.Sign(distance.Y));
                }
            
                positions.Add(knots[^1]);
                repetition--;
            }
        }

        return positions.Count;
    }
    
    protected override int PartOne() => TraceLastKnot(length: 2);

    protected override int PartTwo() => TraceLastKnot(length: 10);
}
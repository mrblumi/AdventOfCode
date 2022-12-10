using System.Text;

namespace AdventOfCode.Puzzles.Of2022;

[Puzzle(2022, 10, "Cathode-Ray Tube")]
public class Day10 : Puzzle<int, string>
{
    private sealed record Register(int Cycle, int Value);

    private IEnumerable<Register> TraceValues()
    {
        var cycle = 1;
        var value = 1;

        return InputLines.SelectMany(_ => _ == "noop" ? Noop() : AddX(_));
        
        IEnumerable<Register> Noop()
        {
            yield return new Register(cycle++, value);
        }
        
        IEnumerable<Register> AddX(string line)
        {
            yield return new Register(cycle++, value);
            yield return new Register(cycle++, value);
            value += Parse(line.Substring(5));
        }
    }

    protected override int PartOne() => TraceValues().Where(_ => _.Cycle % 40 == 20).Sum(_ => _.Cycle * _.Value);

    protected override string PartTwo()
    {
        using var enumerator = TraceValues().GetEnumerator();
        var builder = new StringBuilder(Environment.NewLine);
        
        while (enumerator.MoveNext())
        {
            var current = enumerator.Current;

            builder.Append(current.Cycle % 40 - 2 <= current.Value && current.Value <= current.Cycle % 40 ? '#' : ' ');
            if (current.Cycle % 40 == 0) builder.Append(Environment.NewLine);
        }

        return builder.ToString();
    }
}
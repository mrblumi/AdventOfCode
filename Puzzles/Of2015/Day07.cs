namespace AdventOfCode.Puzzles.Of2015;

[Puzzle(2015, 07, "Some Assembly Required")]
public class Day07 : Puzzle<int>
{
    private IDictionary<string, string> Gates => InputLines
        .Select(_ => _.Split(" -> "))
        .ToDictionary(_ => _[^1], _ => _[0]);

    private int Solve(string wire, IDictionary<string, string> gates)
    {
        ushort Evaluate(string s)
        {
            var alreadySuccessful = ushort.TryParse(s, out var res);
            if (alreadySuccessful) return res;
                
            res = gates[s].Split(" ") switch
            {
                [var x] => Evaluate(x),
                ["NOT", var x] => (ushort)~Evaluate(x),
                [var x, "AND", var y] => (ushort)(Evaluate(x) & Evaluate(y)),
                [var x, "OR", var y] => (ushort)(Evaluate(x) | Evaluate(y)),
                [var x, "LSHIFT", var y] => (ushort)(Evaluate(x) << Evaluate(y)),
                [var x, "RSHIFT", var y] => (ushort)(Evaluate(x) >> Evaluate(y)),
                _ => throw new NotSupportedException(gates[s])
            };

            gates[s] = res.ToString();
            return res;
        }

        return Evaluate(wire);
    }

    protected override int PartOne() => Solve(wire: "a", gates: Gates);

    protected override int PartTwo()
    {
        var gates = Gates;
        gates["b"] = Solve(wire: "a", Gates).ToString();
        
        return Solve(wire: "a", gates);
    }
}
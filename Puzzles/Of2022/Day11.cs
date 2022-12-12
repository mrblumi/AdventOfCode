namespace AdventOfCode.Puzzles.Of2022;

[Puzzle(2022, 11, "Monkey in the Middle")]
public sealed partial class Day11 : Puzzle<long>
{
    private Monkey[] Monkeys() => InputLines.Chunk(7).Select(_ => new Monkey(_)).ToArray();
    
    private void KeepAway(Monkey[] monkeys, Func<int, int> manageAnxiety)
    {
        foreach(var monkey in monkeys)
        {
            while (monkey.TryDequeue(out var worryLevel))
            {
                var nextLevel = monkey.Investigate(worryLevel, manageAnxiety);
                var nextIndex = monkey.NextMonkeyFor(nextLevel);
                    
                monkeys[nextIndex].Enqueue(nextLevel);
            }
        }
    }

    private long MonkeyBusiness(Monkey[] monkeys, int rounds, Func<int, int> manageAnxiety)
    {
        while (rounds-- > 0) KeepAway(monkeys, manageAnxiety);
        return monkeys
            .Select(_ => _.Investigations)
            .OrderByDescending(_ => _)
            .Take(2)
            .Aggregate(seed: 1L, (i, j) => i * j);
    }

    protected override long PartOne() => MonkeyBusiness(Monkeys(), rounds: 20, manageAnxiety: _ => _ / 3);
    
    protected override long PartTwo()
    {
        var monkeys = Monkeys();
        var divisor = monkeys.Aggregate(1, (i, monkey) => i * monkey.Divisor);
        
        // anxiety modulo product of divisors of all monkeys does not change result where item is thrown
        return MonkeyBusiness(monkeys, rounds: 10000, manageAnxiety: _ => _ % divisor);
    }
}
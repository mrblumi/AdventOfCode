namespace AdventOfCode.Puzzles.Of2022;

using static StringSplitOptions;

public sealed partial class Day11
{
    private sealed class Monkey : Queue<int>
    {
        private readonly Func<int, int> _investigate;

        private readonly int _divisor;
        private readonly int _ifTrue;
        private readonly int _ifFalse;
        
        public Monkey(string[] input)
        {
            input[1].Split(new[] {':', ',' }, TrimEntries).Skip(1).Select(Parse).ForEach(Enqueue);

            var operation = input[2].Split(' ', RemoveEmptyEntries)[3..];
            Func<int, int, int> calc = operation[1] == "*" ? (i, j) => i * j : (i, j) => i + j;
            
            _investigate = operation switch
            {
                ["old", _, "old"] => old => calc(old, old),
                ["old", _, var x] => old => calc(old, Parse(x)),
                _ => throw new NotSupportedException()
            };
            
            Divisor = last(input[3]);
            _ifTrue = last(input[4]);
            _ifFalse = last(input[5]);
            
            static int last(string s) => Parse(s.Split(' ')[^1]);
        }
        
        public long Investigations { get; private set; }

        public int Investigate(int worryLevel, Func<int,int> manageAnxiety)
        {
            Investigations++;

            worryLevel = _investigate(worryLevel);
            worryLevel = manageAnxiety(worryLevel);

            return worryLevel;
        }
         
        public int Divisor { get; }
        
        public int NextMonkeyFor(int worryLevel) => worryLevel % Divisor == 0 ? _ifTrue : _ifFalse;
    }
}
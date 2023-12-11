namespace AdventOfCode.Puzzles.Of2015;

[Puzzle(2015, 14, "Reindeer Olympics")]
public class Day14 : Puzzle<int>
{
    private readonly IReadOnlyCollection<State> _states;
    
    public Day14()
    {
        _states = InputLines
            .Select(line => line.Split(" "))
            .Select(line => new Reindeer(Parse(line[3]), Parse(line[6]), Parse(line[13])))
            .Select(reindeer => new State(reindeer))
            .ToArray();

        var remaining = 2503;
        
        while (remaining-- > 0)
        {
            foreach (var state in _states)
            {
                if (state.IsFlying) state.Distance += state.Reindeer.Speed;
                if (--state.RemainingTime == 0)
                {
                    state.IsFlying = !state.IsFlying;
                    state.RemainingTime = state.IsFlying ? state.Reindeer.FlyingTime : state.Reindeer.RestingTime;
                }
            }

            var distance = _states.Select(state => state.Distance).Max();
            _states.Where(state => state.Distance == distance).ForEach(state => state.Points++);
        }       
    }

    protected override int PartOne() => _states.Select(state => state.Distance).Max();

    protected override int PartTwo() => _states.Select(state => state.Points).Max();
    
    private readonly record struct Reindeer(int Speed, int FlyingTime, int RestingTime);

    private record State(Reindeer Reindeer)
    {
        public bool IsFlying { get; set; } = true;
        public int RemainingTime { get; set; } = Reindeer.FlyingTime;
        public int Distance { get; set; }
        public int Points { get; set; }
    }
}
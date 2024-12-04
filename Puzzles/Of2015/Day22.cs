namespace AdventOfCode.Puzzles.Of2015;

[Puzzle(2015, 22, "Wizard Simulator 20XX")]
public class Day22 : Puzzle<int>
{
    private record Boss(
        int HitPoints,
        int Damage);

    private record Player(
        int HitPoints,
        int ManaPoints,
        int Armor = 0);
    
    private record Spell(string Name,
        int Cost,
        int Damage = 0,
        int Armor = 0,
        int RechargeHitPoints = 0,
        int RechargeManaPoints = 0,
        int Duration = 0);

    private record State(
        Player Player,
        Boss Boss,
        Spell[] Effects,
        int SpentManaPoints = 0);
    
    private static readonly Spell[] Spells =
    [
        new("Magic Missile", Cost: 53, Damage: 4),
        new("Drain", Cost: 73, Damage: 2, RechargeHitPoints: 2),
        new("Shield", Cost: 113, Duration: 6, Armor: 7),
        new("Poison", Cost: 173, Duration: 6, Damage: 3),
        new("Recharge", Cost: 229, Duration: 5, RechargeManaPoints: 101),
    ];
    
    private readonly Boss _boss;
    private readonly Player _player;
    
    public Day22()
    {
        var input = InputLines
            .Select(x => x.Split(": ")[^1])
            .Select(x => int.Parse(x))
            .ToArray();

        _boss = new(HitPoints: input[0], Damage: input[1]);
        _player = new(HitPoints: 50, ManaPoints: 500);
    }
    
    private static State ApplyEffects(State state)
    {
        var armor = state.Effects.Sum(x => x.Armor);
        var bossHitPoints = state.Boss.HitPoints - state.Effects.Sum(x => x.Damage);
        var manaPoints = state.Player.ManaPoints + state.Effects.Sum(x => x.RechargeManaPoints);
        var player = state.Player with { ManaPoints = manaPoints, Armor = armor };
        var boss = state.Boss with { HitPoints = bossHitPoints };
        var effects = state.Effects
            .Select(x => x with { Duration = x.Duration - 1 })
            .Where(x => x.Duration > 0)
            .ToArray();

        return state with { Player = player, Boss = boss, Effects = effects };
    }

    private static Func<State, IEnumerable<State>> ApplyPlayersStep(int manaLimit) =>
        state => ApplyPlayersStep(state, manaLimit);
    
    private static IEnumerable<State> ApplyPlayersStep(State state, int manaLimit)
    {
        foreach (var spell in Spells)
        {
            var manaPoints = state.Player.ManaPoints - spell.Cost;
            var spentManaPoints = state.SpentManaPoints + spell.Cost;
            var playerHitPoints = state.Player.HitPoints;
            var bossHitPoints = state.Boss.HitPoints;
            var effects = state.Effects;

            if (state.Effects.Any(x => x.Name == spell.Name)) continue;
            if (manaPoints < 0) continue;
            if (spentManaPoints > manaLimit) continue;
            
            if (spell.Duration == 0)
            {
                playerHitPoints += spell.RechargeHitPoints;
                bossHitPoints -= spell.Damage;
            }
            else effects = [spell, ..effects];

            var player = state.Player with { HitPoints = playerHitPoints, ManaPoints = manaPoints };
            var boss = state.Boss with { HitPoints = bossHitPoints };

            yield return new(player, boss, effects, spentManaPoints);
        }
    }

    private static State ApplyBossesStep(State state)
    {
        var bossesDamage = state.Boss.Damage;
        var playersArmor = state.Player.Armor;
        var playersHitPoints = state.Player.HitPoints - (bossesDamage - playersArmor);
        var player = state.Player with { HitPoints = playersHitPoints };

        return state with { Player = player };
    }

    private static bool TrySolve(State state, int manaLimit, bool hard)
    {
        if (hard) state = state with { Player = state.Player with { HitPoints = state.Player.HitPoints - 1 } };
        if (state.Player.HitPoints <= 0) return false;
        
        return state
            .Then(ApplyEffects)
            .Then(ApplyPlayersStep(manaLimit))
            .Select(ApplyEffects)
            .Select(ApplyBossesStep)
            .Any(x => x.Boss.HitPoints <= 0 || x.Player.HitPoints > 0 && TrySolve(x, manaLimit, hard));
    }

    private static int Solve(State state, bool hard = false)
    {
        var upperLimit = 1;
        
        while (TrySolve(state, manaLimit: upperLimit, hard) is false)
            upperLimit *= 2;
        
        var lowerLimit = upperLimit / 2;
        
        while (upperLimit - lowerLimit > 1)
        {
            var middle = (upperLimit + lowerLimit) / 2;
            
            if (TrySolve(state, manaLimit: middle, hard)) upperLimit = middle;
            else lowerLimit = middle;
        }
        
        return upperLimit;
    }
    
    protected override int PartOne() => Solve(new(_player, _boss, []));
    protected override int PartTwo() => Solve(new(_player, _boss, []), hard: true);
}
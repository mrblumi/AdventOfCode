namespace AdventOfCode.Puzzles.Of2015;

[Puzzle(2015, 21, "RPG Simulator 20XX")]
public class Day21 : Puzzle<int>
{
    private readonly record struct Equipment(int Damage, int Armor, int Price = 0);
    private readonly record struct Player(int HitPoints, Equipment Equipment);

    private readonly List<Player> _combinations = new();

    public Day21()
    {
        var shop = new Dictionary<string, Equipment[]>()
        {
            ["Weapons"] = [
                new(4,0, 08),
                new(5,0, 10),
                new(6,0, 25),
                new(7,0, 40),
                new(8,0, 74),
            ],
            ["Armor"] = [
                new(0,1, 013),
                new(0,2, 031),
                new(0,3, 053),
                new(0,4, 075),
                new(0,5, 102),
            ],
            ["Rings"] = [
                new(1,0, 025),
                new(2,0, 050),
                new(3,0, 100),
                new(0,1, 020),
                new(0,2, 040),
                new(0,3, 080),
            ]
        };
        
        var combinations =
            from weapon in shop["Weapons"]
            from armor in shop["Armor"].PowerSet().Where(x => x.Length <= 1)
            from rings in shop["Rings"].PowerSet().Where(x => x.Length <= 2)
            select new List<Equipment>([ weapon, ..armor, ..rings ]);

        foreach (var combination in combinations)
            _combinations.Add(new(
                HitPoints: 100,
                Equipment: new(
                    Damage: combination.Sum(x => x.Damage),
                    Armor: combination.Sum(x => x.Armor),
                    Price: combination.Sum(x => x.Price))));
    }

    protected override int PartOne() => _combinations.Where(You(win: true)).Select(x => x.Equipment.Price).Min();
    protected override int PartTwo() => _combinations.Where(You(win: false)).Select(x => x.Equipment.Price).Max();
    
    private static Func<Player, bool> You(bool win) => player =>
    {
        var boss = new Player(HitPoints: 100, Equipment: new(Damage: 8, Armor: 2));

        while (boss.HitPoints > 0 && player.HitPoints > 0)
        {
            boss = boss with { HitPoints = boss.HitPoints - Max(1, player.Equipment.Damage - boss.Equipment.Armor) };
            player = player with { HitPoints = player.HitPoints - Max(1, boss.Equipment.Damage - player.Equipment.Armor) };

            if (boss.HitPoints <= 0) return win;
            if (player.HitPoints <= 0) return !win;
        }

        return default;
    };
}
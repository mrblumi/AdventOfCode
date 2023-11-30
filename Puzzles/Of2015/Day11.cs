namespace AdventOfCode.Puzzles.Of2015;

[Puzzle(2015, 11, "Corporate Policy")]
public class Day11 : Puzzle<string>
{
    private const string Password = "hepxcrrq";

    private static void Increment(char[] password)
    {
        for (var i = password.Length - 1; i >= 0; i--)
        {
            password[i] = password[i] switch { 'z' => 'a', _ => ++password[i] };
            if (password[i] is not 'a') break;
        }
    }
    
    private static IEnumerable<string> Passwords()
    {
        var password = Password.ToArray();
        
        while (true)
        {
            Increment(password);
            yield return new(password);
        }
    }

    private static bool IsValid(string password) =>
        password.All(_ => _ is not 'i' and not 'o' and not 'l') &&
        password[..^1].Zip(password[1..]).Where(_ => _.First == _.Second).Distinct().Skip(1).Any() &&
        password[..^2].Zip(password[1..^1], password[2..]).Any(_ => _.First == _.Second - 1 && _.Second == _.Third - 1);

    protected override string PartOne() => Passwords().Where(IsValid).First();
    protected override string PartTwo() => Passwords().Where(IsValid).Skip(1).First();
}
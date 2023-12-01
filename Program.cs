using System.Reflection;

const string help = """
                    --year      the year of the puzzle
                    --day       the day of the puzzle
                    --help      print this information
                    """;

if (args is [] || args.Contains("--help"))
{
    Console.WriteLine(help);
    return;
}

var year = Parse(args[Array.IndexOf(args, "--year") + 1]);
var day = Parse(args[Array.IndexOf(args, "--day") + 1]);

var puzzle = typeof(Program).Assembly
    .GetTypes()
    .Where(_ => _.IsAssignableTo(typeof(IPuzzle)) && _.IsAbstract == false)
    .Select(_ => (Type: _, Attribute: _.GetCustomAttribute<PuzzleAttribute>()!))
    .Where(_ => _.Attribute.Year == year && _.Attribute.Day == day)
    .Select(_ => Activator.CreateInstance(_.Type))
    .Cast<IPuzzle>()
    .Single();

puzzle.Solve();
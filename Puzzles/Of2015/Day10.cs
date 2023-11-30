using System.Text;

namespace AdventOfCode.Puzzles.Of2015;

[Puzzle(2015, 10, "Elves Look, Elves Say")]
public class Day10 : Puzzle<int>
{
    private const string Input = "1113122113";

    private static string Apply(string input)
    {
        var stringBuilder = new StringBuilder();
        char character = input[0];
        int count = 1;
        
        foreach (var current in input[1..])
        {
            if (current == character)
            {
                count++;
                continue;
            }

            stringBuilder.Append(count);
            stringBuilder.Append(character);

            character = current;
            count = 1;
        }

        stringBuilder.Append(count);
        stringBuilder.Append(character);
        
        return stringBuilder.ToString();
    }

    private static string Apply(string input, int count)
    {
        while (count-- > 0) input = Apply(input);
        return input;
    }


    protected override int PartOne() => Apply(Input, 40).Length;
    protected override int PartTwo() => Apply(Input, 50).Length;
}
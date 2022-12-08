namespace AdventOfCode.Puzzles.Of2015;

[Puzzle(2015, 01, "Not Quite Lisp")]
public sealed class Day01 : Puzzle<int>
{
    private readonly int _finalFloor;
    private readonly int _basementAccess;
    
    public Day01()
    {
        var input = InputText;
        var floor = 0;
        
        for (var i = 0; i < input.Length; i++)
        {
            floor += input[i] switch { '(' => 1, ')' => -1, _ => 0 };
            if (_basementAccess == 0 && floor < 0) _basementAccess = i + 1;
        }

        _finalFloor = floor;
    }

    protected override int PartOne() => _finalFloor;
    protected override int PartTwo() => _basementAccess;
}
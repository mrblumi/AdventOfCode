namespace AdventOfCode.Puzzles.Of2015;

public sealed record Day01 : Puzzle
{
    private readonly int _finalFloor;
    private readonly int _basementAccess;
    
    public Day01() : base(Year: 2015, Day: 01, "Not Quite Lisp")
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

    protected override object PartOne() => _finalFloor;
    protected override object PartTwo() => _basementAccess;
}
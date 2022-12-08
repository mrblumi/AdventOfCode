namespace AdventOfCode.Commons;

public class PuzzleAttribute : Attribute
{
    public PuzzleAttribute(int year, int day, string description)
    {
        Year = year;
        Day = day;
        Description = description;
    }

    public int Year { get; }
    public int Day { get; }
    public string Description { get; }
}
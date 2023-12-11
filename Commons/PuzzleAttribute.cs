namespace AdventOfCode.Commons;

public class PuzzleAttribute(int year, int day, string description) : Attribute
{
    public int Year { get; } = year;
    public int Day { get; } = day;
    public string Description { get; } = description;
}
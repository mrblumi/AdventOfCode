using System.Text.Json.Nodes;

namespace AdventOfCode.Puzzles.Of2022;

[Puzzle(2022, 13, "Distress Signal")]
public class Day13 : Puzzle<int>
{
    private readonly List<JsonNode> _input;

    public Day13() => _input = InputLines
        .Where(_ => !string.IsNullOrEmpty(_))
        .Select(_ => JsonNode.Parse(_)!)
        .ToList(); 

    static int CompareNodes(JsonNode? first, JsonNode? second) => (first, second) switch
    {
        (JsonValue x, JsonValue y) => (int)x - (int)y,
        (JsonValue x, JsonArray y) => CompareArrays(new JsonArray((int)x), y),
        (JsonArray x, JsonValue y) => CompareArrays(x, new JsonArray((int)y)),
        (JsonArray x, JsonArray y) => CompareArrays(x, y),
        _ => throw new NotSupportedException()
    };

    static int CompareArrays(JsonArray first, JsonArray second) => Enumerable
        .Zip(first, second)
        .Select(_ => CompareNodes(_.First, _.Second))
        .FirstOrDefault(_ => _ != 0, first.Count - second.Count);
    
    protected override int PartOne() => _input
        .Chunk(2)
        .Select((chunk, index) => (rightOrder: CompareNodes(chunk[0], chunk[1]) < 0, index))
        .Where(_ => _.rightOrder)
        .Sum(_ => _.index  + 1);

    protected override int PartTwo()
    {
        var two = JsonNode.Parse("[[2]]")!;
        var six = JsonNode.Parse("[[6]]")!;
        
        _input.Add(two);
        _input.Add(six);
        _input.Sort(CompareNodes);

        return (_input.IndexOf(two) + 1) * (_input.IndexOf(six) + 1);
    }
}
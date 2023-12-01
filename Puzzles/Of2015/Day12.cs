using System.Text.Json;

namespace AdventOfCode.Puzzles.Of2015;

[Puzzle(2015, 12, "JSAbacusFramework.io")]
public class Day12 : Puzzle<int>
{
    private readonly JsonElement _jsonElement;

    public Day12() => _jsonElement = JsonDocument.Parse(InputText).RootElement;
    
    private static IEnumerable<int> Traverse(JsonElement json, string? ignore = null)
    {
        if (json.ValueKind is JsonValueKind.Number) yield return json.GetInt32();
        
        if (json.ValueKind is JsonValueKind.Object)
            if (ignore is not null && json.EnumerateObject().Select(Value).Any(HasValue(ignore))) yield break;
            else foreach (var res in json.EnumerateObject().Select(Value).SelectMany(Traverse(ignore))) yield return res;
        
        if (json.ValueKind is JsonValueKind.Array)
            foreach (var res in json.EnumerateArray().SelectMany(Traverse(ignore))) yield return res;
    }

    private static Func<JsonElement, IEnumerable<int>> Traverse(string? ignoreValue) =>
        json => Traverse(json, ignoreValue);

    private static Func<JsonElement, bool> HasValue(string value) =>
        json => json.ValueKind is JsonValueKind.String && json.GetString() == value;

    private static JsonElement Value(JsonProperty json) => json.Value;
    
    protected override int PartOne() => Traverse(_jsonElement).Sum();
    protected override int PartTwo() => Traverse(_jsonElement, ignore: "red").Sum();
}
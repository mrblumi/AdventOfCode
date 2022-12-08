namespace AdventOfCode.Puzzles.Of2022;

[Puzzle(2022, 07, "No Space Left On Device")]
public sealed class Day07 : Puzzle<int>
{
    private interface INode { string Name { get; } int Size { get; } }

    private sealed record File(string Name, int Size) : INode;

    private sealed class Directory : List<INode>, INode
    {
        private int? _size;
        public int Size => _size ??= this.Sum(_ => _.Size);

        public string Name { get; }
        public Directory? Parent { get; }

        public Directory(string name, Directory? parent = null)
        {
            Name = name;
            Parent = parent;
        }
    }

    private readonly List<Directory> _directories = new();

    public Day07()
    {
        var currentDir = new Directory(name: "/");
        _directories.Add(currentDir);

        foreach (var line in InputLines.Skip(1))
        {
            if (line.StartsWith("dir")) continue;
            if (line == "$ ls") continue;

            if (line == "$ cd ..")
            {
                currentDir = currentDir.Parent!;
                continue;
            }

            if (line.StartsWith("$ cd "))
            {
                currentDir = new(name: line.Split(' ')[2], parent: currentDir);
                currentDir.Parent!.Add(currentDir);
                _directories.Add(currentDir);
                continue;
            }

            var split = line.Split(' ');
            currentDir.Add(new File(Name: split[1], Size: Parse(split[0])));
        }
    }

    protected override int PartOne() => _directories.Where(_ => _.Size < 100000).Sum(_ => _.Size);
    
    protected override int PartTwo()
    {
        var rootDirectorySize = _directories.First().Size;
        var requiredSpace = 30000000 - 70000000 + rootDirectorySize;
        
        return _directories.OrderBy(_ => _.Size).First(_ => _.Size > requiredSpace).Size;
    }
}
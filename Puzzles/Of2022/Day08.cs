namespace AdventOfCode.Puzzles.Of2022;

[Puzzle(2022, 08, "Treetop Tree House")]
public class Day08 : Puzzle<int>
{
    private sealed record Tree(int X, int Y, int Size)
    {
        public bool FromNorth(Tree other) => Y == other.Y && X > other.X;
        public bool FromSouth(Tree other) => Y == other.Y && X < other.X;
        public bool FromWest(Tree other) => X == other.X && Y > other.Y;   
        public bool FromEast(Tree other) => X == other.X && Y < other.Y;  
    }

    private static Func<char, int, Tree> Trees(int x) => (size, y) => new Tree(x, y, Size: size - 48);
    private static IEnumerable<Tree> Trees(string line, int x) => line.ToCharArray().Select(Trees(x));
    
    private readonly IReadOnlyCollection<Tree> _trees;

    public Day08() => _trees = InputLines.SelectMany(Trees).ToArray();

    protected override int PartOne() => _trees.Count(tree =>
        _trees.Where(tree.FromNorth).All(_ => tree.Size > _.Size) ||
        _trees.Where(tree.FromSouth).All(_ => tree.Size > _.Size) ||
        _trees.Where(tree.FromWest).All(_ => tree.Size > _.Size)  ||
        _trees.Where(tree.FromEast).All(_ => tree.Size > _.Size));

    protected override int PartTwo()
    {
        Tree? FirstBlockingTree(Tree tree, Func<Tree, bool> inDirection, Func<Tree, int> orderBy) =>
            _trees.Where(inDirection).OrderBy(orderBy).FirstOrDefault(_ => _.Size >= tree.Size);

        var last = _trees.Last();
        
        return _trees
            .Select(tree => 
                (tree.X != 0 ? tree.X - FirstBlockingTree(tree, tree.FromNorth, _ => -_.X)?.X ?? tree.X : 0) *
                (tree.X != last.X ? (FirstBlockingTree(tree, tree.FromSouth, _ => _.X)?.X ?? last.X) - tree.X : 0) *
                (tree.Y != 0 ? tree.Y - FirstBlockingTree(tree, tree.FromWest, _ => -_.Y)?.Y ?? tree.Y : 0) *
                (tree.Y != last.Y ? (FirstBlockingTree(tree, tree.FromEast, _ => _.Y)?.Y ?? last.Y) - tree.Y : 0))
            .Max();
    }
}
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Puzzles.Of2015;

[Puzzle(2015, 04, "The Ideal Stocking Stuffer")]
public sealed class Day04 : Puzzle<int>
{
    private const string Secret = "yzbqklnj";
    private static readonly MD5 Md5 = MD5.Create();

    private readonly int _withFive = 0; 
    private readonly int _withSix = 0; 

    public Day04()
    {
        using var enumerator = Enumerable
            .Range(0, Int32.MaxValue)
            .Select(_ => new Value(_))
            .GetEnumerator();
        
        while (_withFive == 0 || _withSix == 0)
        {
            enumerator.MoveNext();
            var hexString = enumerator.Current.HexString;

            if (_withFive == 0 && hexString.StartsWith("00000")) _withFive = enumerator.Current.Number;
            if (_withSix == 0 && hexString.StartsWith("000000")) _withSix = enumerator.Current.Number;
        }
    }

    protected override int PartOne() => _withFive;
    protected override int PartTwo() => _withSix;
    
    private sealed record Value(int Number)
    {
        public string HexString => GetHexString();
        
        private string GetHexString()
        {
            var bytes = Encoding.ASCII.GetBytes($"{Secret}{Number}");
            var hash = Md5.ComputeHash(bytes);
            return Convert.ToHexString(hash);
        }
    }
}
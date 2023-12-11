using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzles.Of2023;

[Puzzle(2023, 07, "Camel Cards")]
public class Day07 : Puzzle<int>
{
    private readonly IReadOnlyCollection<Hand> _hands;

    public Day07() => _hands = InputLines
        .Select(line => line.Split(" "))
        .Select(hand => new Hand(hand[0], Parse(hand[1])))
        .ToArray();

    protected override int PartOne() => Solve(new Comparer());
    protected override int PartTwo()
    {
        static void HandleJokers(Dictionary<char, int> hand)
        {
            if (hand is { Count: 1 }) return;
            if (hand.TryGetValue('J', out var amount)) hand.Remove('J');
            else return;
            
            hand[hand.MaxBy(card => card.Value).Key] += amount;
        }
        
        return Solve(new Comparer(HandleJokers, valueOfJoker: 0));
    }
    
    private int Solve(IComparer<Hand> comparer) => _hands
        .Order(comparer)
        .Select((hand, rank) => hand.Bid * (rank + 1))
        .Sum();
    
    private readonly record struct Hand
    {
        public Hand(string cards, int bid)
        {
            if (Regex.IsMatch(cards, "^[0-9TJQKA]{5}$")) Cards = cards;
            else throw new FormatException($"{nameof(Cards)}: {cards}");

            Bid = bid;
        }

        public string Cards { get; }
        public int Bid { get; }
    }
    
    private class Comparer(
        Action<Dictionary<char, int>>? adjust = null,
        int valueOfJoker = 11
        ) : IComparer<Hand>
    {
        public int Compare(Hand x, Hand y)
        {
            var self = GetCardsType(x.Cards);
            var other = GetCardsType(y.Cards);
            
            if (self < other) return -1;
            if (self > other) return 1;

            return x.Cards
                .Zip(y.Cards)
                .Select(tuple => (First: GetCardValue(tuple.First), Second: GetCardValue(tuple.Second)))
                .Select(tuple => tuple.First.CompareTo(tuple.Second))
                .FirstOrDefault(result => result != 0);
        }
        
        private int GetCardValue(char card) =>
            card switch { 'T' => 10, 'J' => valueOfJoker, 'Q' => 12, 'K' => 13, 'A' => 14, _ => Parse(card.ToString()) };
        
        private Type GetCardsType(string hand)
        {
            var cards = hand
                .GroupBy(card => card)
                .ToDictionary(group => group.Key, group => group.Count());
                
            adjust?.Invoke(cards);
                
            return cards
                .OrderByDescending(kvp => kvp.Value)
                .Select(kvp => kvp.Value)
                .ToArray() switch
                {
                    [5] => Type.FiveOfAKind,
                    [4, ..] => Type.FourOfAKind,
                    [3, 2] => Type.FullHouse,
                    [3, ..] => Type.ThreeOfAKind,
                    [2, 2, ..] => Type.TwoPair,
                    [2, ..] => Type.OnePair,
                    _ => Type.HighCard
                };
        }
    }
    
    private enum Type { HighCard, OnePair, TwoPair, ThreeOfAKind, FullHouse, FourOfAKind, FiveOfAKind }
}
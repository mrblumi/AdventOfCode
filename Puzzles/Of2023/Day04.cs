namespace AdventOfCode.Puzzles.Of2023;

[Puzzle(2023, 04, "Scratchcards")]
public class Day04 : Puzzle<int>
{
    private readonly IReadOnlyCollection<Scratchcard> _scratchcards;

    public Day04() => _scratchcards = InputLines
        .Select((line, index) => (Index: index + 1, Parts: line.Split(':', '|')))
        .Select(card => new Scratchcard(card.Index, Numbers(card.Parts[1]), Numbers(card.Parts[2])))
        .ToArray();

    protected override int PartOne() => _scratchcards
        .Select(card => card.AmountOfWinningNumbersYouHave)
        .Where(amount => amount > 0)
        .Select(amount => (int)Math.Pow(2, amount - 1))
        .Sum();

    protected override int PartTwo()
    {
        var amountOfCards = new Dictionary<int, int>();

        foreach (var card in _scratchcards)
        {
            var amount = Add(amountOfCards, card.Id, 1);
            
            if (card.AmountOfWinningNumbersYouHave > 0) Enumerable
                .Range(card.Id + 1, card.AmountOfWinningNumbersYouHave)
                .ForEach(cardId => Add(amountOfCards, cardId, amount));
        }

        return amountOfCards.Values.Sum();
    }
    
    private static int[] Numbers(string numbers) => numbers
        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Select(Parse)
        .ToArray();

    private static int Add(IDictionary<int, int> amountOfCards, int cardId, int amount)
    {
        amountOfCards.TryAdd(cardId, 0);
        return amountOfCards[cardId] += amount;
    }
    
    private readonly struct Scratchcard(int id, int[] winningNumbers, int[] numbersYouHave)
    {
        public int Id { get; } = id;
        public int AmountOfWinningNumbersYouHave { get; } = winningNumbers.Intersect(numbersYouHave).Count();
    }
}
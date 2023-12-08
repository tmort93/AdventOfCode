using System.Collections;
using AdventOfCode.Common;

namespace AdventOfCode.DaySeven;

public class Puzzle : PuzzleBase
{
    protected override string DayPart => "DaySeven";

    /// <summary>
    /// Answer: 253910319
    /// </summary>
    public override async Task<int> SolvePartOneAsync()
    {
        var lines = File.ReadLinesAsync(GetPuzzleInputFilePath);
        var hands = new List<Hand>();
        await foreach (var line in lines)
        {
            var split = line.Split(' ');
            var hand = new Hand
            {
                Bid = int.Parse(split[1])
            };
            foreach (var cardText in split[0])
            {
                var cardNumber = cardText switch
                {
                    'A' => 14,
                    'K' => 13,
                    'Q' => 12,
                    'J' => 11,
                    'T' => 10,
                    _ => int.Parse(cardText.ToString())
                };
                hand.Cards.Add(cardNumber);
            }
            var uniqueCards = hand.Cards.Distinct();
            var uniqueCardCount = uniqueCards.Count();
            var type = uniqueCardCount switch
            {
                5 => HandType.HighCard,
                4 => HandType.OnePair,
                1 => HandType.FiveKind,
                _ => HandType.Unknown
            };
            if (uniqueCardCount == 2)
            {
                foreach (var uniqueCard in uniqueCards)
                {
                    if (hand.Cards.Count(c => c == uniqueCard) == 4)
                    {
                        type = HandType.FourKind;
                        break;
                    }
                }
                if (type == HandType.Unknown)
                {
                    type = HandType.FullHouse;
                }
            }
            if (uniqueCardCount == 3)
            {
                foreach (var uniqueCard in uniqueCards)
                {
                    if (hand.Cards.Count(c => c == uniqueCard) == 3)
                    {
                        type = HandType.ThreeKind;
                        break;
                    }
                }
                if (type == HandType.Unknown)
                {
                    type = HandType.TwoPair;
                }
            }
            hand.Type = type;

            hands.Add(hand);
        }

        hands.Sort(new HandComparer());
        // hands.Reverse();

        var winnings = 0;
        for (var i = 1; i <= hands.Count; i++)
        {
            var handValue = hands[i - 1].Bid * i;
            winnings += handValue;
        }

        return winnings;
    }

    /// <summary>
    /// Answer: 
    /// </summary>
    public override async Task<int> SolvePartTwoAsync()
    {
        return -1;
    }
}

public enum HandType
{
    Unknown,
    HighCard,
    OnePair,
    TwoPair,
    ThreeKind,
    FullHouse,
    FourKind,
    FiveKind
}

public class Hand
{
    public int Bid;
    public HandType Type;
    public List<int> Cards = [];
}

public class HandComparer : IComparer<Hand>
{
    public int Compare(Hand? x, Hand? y)
    {
        if (x == null)
            return -1;

        if (y == null)
            return 1;

        if (x.Type > y.Type)
            return 1;

        if (x.Type == y.Type)
        {
            for (var i = 0; i < x.Cards.Count; i++)
            {
                if (x.Cards[i] > y.Cards[i])
                    return 1;

                if (x.Cards[i] < y.Cards[i])
                    return -1;
            }
            return 0;
        }

        return -1;
    }
}

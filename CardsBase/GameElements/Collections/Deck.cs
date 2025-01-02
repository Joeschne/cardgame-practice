using CardsBase.GameElements.Cards;

namespace CardsBase.GameElements.Collections;

public class Deck() : CardCollection(CardVisibility.Hidden)
{
    private readonly Random _random = new();

    internal void Shuffle()
    {
        for (int i = Cards.Count - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);
            (_cards[i], _cards[j]) = (_cards[j], _cards[i]);
        }
    }
}

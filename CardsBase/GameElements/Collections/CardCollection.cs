using CardsBase.GameElements.Cards;
using CardsBase.GameElements.Players;

namespace CardsBase.GameElements.Collections;

public abstract class CardCollection
{
    internal protected readonly List<Card> _cards = new();
    internal protected IReadOnlyCollection<Card> Cards => _cards.AsReadOnly();

    internal CardVisibility Visibility { get; set; }

    internal CardCollection(CardVisibility visibility)
    {
        Visibility = visibility;
    }

    internal virtual void AddCard(Card card) => _cards.Add(card);
    internal virtual void RemoveCard(Card card) => _cards.Remove(card);
    internal virtual void InsertCardAt(Card card, int position)
    {
        if (position < 0 || position > _cards.Count)
            throw new ArgumentOutOfRangeException("Invalid card insert position");

        _cards.Insert(position, card);
    }
}


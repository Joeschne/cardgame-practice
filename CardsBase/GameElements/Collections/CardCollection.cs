using CardsBase.GameElements.Cards;

namespace CardsBase.GameElements.Collections;

public abstract class CardCollection
{
    public List<Card> Cards = new();
    public virtual void AddCard(Card card) => Cards.Add(card);
    public virtual void RemoveCard(Card card) => Cards.Remove(card);
}

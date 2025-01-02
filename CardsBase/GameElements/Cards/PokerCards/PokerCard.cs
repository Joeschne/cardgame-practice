namespace CardsBase.GameElements.Cards.PokerCards;

public class PokerCard : Card
{
    internal PokerCardValue Value { get; }
    internal PokerCardSuit Suit { get; }
    public PokerCard(PokerCardValue value, PokerCardSuit suit) : base()
    {
        Value = value;
        Suit = suit;
        Name = value.ToString() + " of " + suit.ToString();
    }
}

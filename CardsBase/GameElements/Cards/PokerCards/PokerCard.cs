namespace CardsBase.GameElements.Cards.PokerCards;

public class PokerCard : Card
{
    PokerCardValue Value { get;}
    PokerCardSuit Suit { get;}
    public PokerCard(PokerCardValue value, PokerCardSuit suit) : base()
    {
        Value = value;
        Suit = suit;
        Name = value.ToString() + "of" + suit.ToString();
    }
}

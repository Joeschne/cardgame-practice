using CardsBase.GameElements.Cards.PokerCards;
using CardsBase.GameElements.Collections;
using CardsBase.Logic;

namespace CardsBase.GameElements.Factories;

public class PokerCardFactory
{
    private CardManager _cardManager;
    internal PokerCardFactory(CardManager cardManager)
    {
        _cardManager = cardManager;
    }
    public void CreateCard(PokerCardValue value, PokerCardSuit suit, CardCollection initialLocation)
    {
        PokerCard card = new PokerCard(value, suit);
        _cardManager.RegisterCard(card, initialLocation);
    }
}

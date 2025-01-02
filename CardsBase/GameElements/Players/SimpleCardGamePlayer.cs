using CardsBase.GameElements.Cards;
using CardsBase.GameElements.Collections;
using CardsBase.Logic;

namespace CardsBase.GameElements.Players;

public class SimpleCardGamePlayer : Player
{
    public Hand Hand { get; } = new();
    public Deck SharedDeck { get; }
    public SimpleCardGamePlayer (CardManager manager, string name, bool isAI, Deck sharedDeck) : base (manager, name, isAI)
    {
        SharedDeck = sharedDeck;
    }
    public void DrawCard(int amount = 1)
    {
        _cardManager.DrawCard(SharedDeck, Hand, amount);
    }
    public override void ResetPlayer()
    {
        for (int i = Hand.Cards.Count - 1; i >= 0; i--)
        {
            var card = Hand.Cards.ElementAt(i); 
            _cardManager.MoveCard(card, SharedDeck);
        }
    }
}

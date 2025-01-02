using CardsBase.GameElements.Cards;
using CardsBase.GameElements.Cards.PokerCards;
using CardsBase.GameElements.Collections;
using CardsBase.GameElements.Factories;
using CardsBase.GameElements.Players;

namespace CardsBase.Logic;

public class BlackJackManager
{
    private Deck _deck = new();
    private CardManager _cardManager = new();
    private SimpleCardGamePlayer[] _players;
    private SimpleCardGamePlayer _dealer;
    public BlackJackManager (int playerAmt, params string[] names)
    {
        AddDeck();
        _deck.Shuffle();
        _players = new SimpleCardGamePlayer[playerAmt];
        InitializePlayers(names);
        _dealer = new SimpleCardGamePlayer(_cardManager, "Dealer", true, _deck);
    }

    public void PlayGame()
    {
        while (true)
        {
            foreach (var player in _players)
            {
                player.DrawCard(2);
                foreach (Card card in player.Hand.Cards)
                {
                    Console.WriteLine(card);
                }
                player.ResetPlayer();
                Console.ReadKey(true);
            }
        }
    }

    private void InitializePlayers(string[] names)
    {
        for (int i = 0; i < _players.Length; i++)
        {
            _players[i] = new SimpleCardGamePlayer(_cardManager, names[i], false, _deck);
        }
    }
    private void AddDeck()
    {
        PokerCardFactory cardFactory = new PokerCardFactory(_cardManager);
        foreach (PokerCardSuit suit in Enum.GetValues<PokerCardSuit>())
        {
            foreach (PokerCardValue value in Enum.GetValues<PokerCardValue>())
            {
                cardFactory.CreateCard(value, suit, _deck);
            }
        }
    }
}

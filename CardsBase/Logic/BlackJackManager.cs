using CardsBase.GameElements.Cards;
using CardsBase.GameElements.Cards.PokerCards;
using CardsBase.GameElements.Collections;
using CardsBase.GameElements.Factories;
using CardsBase.GameElements.Players;
using CardsBase.Logic;

public class BlackJackManager
{
    private Deck _deck = new();
    private CardManager _cardManager = new();
    public SimpleCardGamePlayer Dealer { get; private set; }

    public event Action DealerTurnStarted;
    public event Action DealerHit; 
    public event Action<int> DealerDone;

    public SimpleCardGamePlayer[] Players { get; private set; }

    public BlackJackManager(int playerAmt, params string[] names)
    {
        AddDeck();
        _deck.Shuffle();
        Players = new SimpleCardGamePlayer[playerAmt];
        InitializePlayers(names);
        Dealer = new SimpleCardGamePlayer(_cardManager, "Dealer", true, _deck);
    }

    public void InitializePlayers(string[] names)
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i] = new SimpleCardGamePlayer(_cardManager, names[i], false, _deck);
        }
    }

    public void AddDeck()
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

    public void StartRound()
    {
        foreach (var player in Players)
        {
            player.DrawCard(2);
        }
        Dealer.DrawCard(2);
    }

    public Card GetDealerVisibleCard() => Dealer.Hand.Cards[0];

    public bool PlayerTurn(SimpleCardGamePlayer player)
    {
        // Check for natural blackjack
        if (GetHandValue(player.Hand.Cards) == 21 && player.Hand.Cards.Count == 2)
        {
            return false; // Turn ends immediately
        }

        return true; // Player can choose to hit or stand
    }

    public bool Hit(SimpleCardGamePlayer player)
    {
        player.DrawCard(1);
        return GetHandValue(player.Hand.Cards) > 21; // Return true if the player busts
    }

    public int GetHandValue(IEnumerable<Card> cards)
    {
        int total = 0;
        int aceCount = 0;

        foreach (PokerCard card in cards)
        {
            if (card.Value == PokerCardValue.Ace)
            {
                aceCount++;
                total += 11; // Assume Ace is 11 initially
            }
            else if ((int)card.Value >= 10)
            {
                total += 10; // Face cards are worth 10
            }
            else
            {
                total += (int)card.Value;
            }
        }

        // Adjust for Aces (if total > 21, downgrade Ace from 11 to 1)
        while (total > 21 && aceCount > 0)
        {
            total -= 10;
            aceCount--;
        }

        return total;
    }

    public void DealerTurn()
    {
        DealerTurnStarted?.Invoke(); // Notify subscribers that the dealer's turn started

        while (GetHandValue(Dealer.Hand.Cards) < 17)
        {
            Dealer.DrawCard(1);
            DealerHit?.Invoke(); // Notify subscribers after each hit
        }

        int finalValue = GetHandValue(Dealer.Hand.Cards);
        DealerDone?.Invoke(finalValue); // Notify subscribers when the dealer stands
    }

    public string GetGameResult(SimpleCardGamePlayer player)
    {
        int playerTotal = GetHandValue(player.Hand.Cards);
        int dealerTotal = GetHandValue(Dealer.Hand.Cards);

        if (playerTotal == 21 && player.Hand.Cards.Count == 2)
            return "Megawin";
        if (playerTotal > 21 || (dealerTotal > playerTotal && dealerTotal <= 21))
            return "Lose";
        if (dealerTotal > 21 || playerTotal > dealerTotal)
            return "Win";
        return "Push";

    }

    public void ResetGame()
    {
        foreach (var player in Players)
        {
            player.ResetPlayer();
        }
        Dealer.ResetPlayer();
        _deck.Shuffle();
    }
}
internal class BlackJackCLI
{
    private BlackJackManager _blackJackManager;

    public void ShowMenu()
    {
        Console.WriteLine("Welcome to Blackjack!");
        Console.Write("Enter number of players: ");
        int playerAmt = int.Parse(Console.ReadLine() ?? "0");

        string[] playerNames = new string[playerAmt];
        for (int i = 0; i < playerAmt; i++)
        {
            Console.Write($"Enter name for Player {i + 1}: ");
            playerNames[i] = Console.ReadLine() ?? $"Player {i + 1}";
        }

        _blackJackManager = new BlackJackManager(playerAmt, playerNames);

        // Subscribe to dealer events
        _blackJackManager.DealerTurnStarted += OnDealerTurnStarted;
        _blackJackManager.DealerHit += OnDealerHit;
        _blackJackManager.DealerDone += OnDealerStand;

        while (true)
        {
            PlayRound();
            Console.WriteLine("Play another round? (y/n): ");
            if (Console.ReadKey(true).KeyChar != 'y')
                break;

            _blackJackManager.ResetGame();
        }
    }

    private void OnDealerTurnStarted()
    {
        Console.WriteLine("\nDealer's turn starts.");
        Console.WriteLine($"Dealer's initial hand: {string.Join(", ", _blackJackManager.Dealer.Hand.Cards)}");
    }

    private void OnDealerHit()
    {
        Console.WriteLine($"Dealer's hand: {string.Join(", ", _blackJackManager.Dealer.Hand.Cards)}");
    }

    private void OnDealerStand(int finalScore)
    {
        if (finalScore <= 21) Console.WriteLine($"Dealer stands. Final score: {finalScore}");
        else Console.WriteLine("Dealer busts");
    }

    private void PlayRound()
    {
        _blackJackManager.StartRound();

        
        foreach (var player in _blackJackManager.Players)
        {
            Console.Clear();
            Console.WriteLine($"Dealer shows: {_blackJackManager.GetDealerVisibleCard()}");
            Console.WriteLine($"{player.Name}'s turn:");

            bool turnActive = _blackJackManager.PlayerTurn(player);
            if (!turnActive)
            {
                Console.WriteLine($"Your hand: {string.Join(", ", player.Hand.Cards)}");
                Console.WriteLine("Natural Blackjack :)\nPress any key to continue");
                Console.ReadKey(true);
            }
            while (turnActive)
            {
                Console.WriteLine($"Your hand: {string.Join(", ", player.Hand.Cards)}");
                Console.WriteLine("Hit or Stand? (h/s): ");

                char choice = Console.ReadKey(true).KeyChar;
                if (choice == 'h' || choice == 'H')
                {
                    if (_blackJackManager.Hit(player))
                    {
                        Console.WriteLine($"Your hand: {string.Join(", ", player.Hand.Cards)}");
                        Console.WriteLine("Bust! You went over 21.\nPress any key to continue");
                        Console.ReadKey(true);
                        turnActive = false;
                    }
                }
                else if (choice == 's' || choice == 'S')
                {
                    turnActive = false;
                }
            }
        }

        Console.WriteLine("Dealer's turn...");
        _blackJackManager.DealerTurn();

        foreach (var player in _blackJackManager.Players)
        {
            string result = _blackJackManager.GetGameResult(player);
            Console.WriteLine($"{player.Name}: {result}");
        }
    }
}

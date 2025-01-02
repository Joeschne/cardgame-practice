using CardsBase.Logic;

namespace CardsBase.CLI;

internal class Menu
{
    public static void ShowMenu()
    {
        Console.WriteLine("What do you wanna do?");
        BlackJackCLI blackJackCLI = new();
        blackJackCLI.ShowMenu();
    }

}

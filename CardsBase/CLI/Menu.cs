using CardsBase.Logic;

namespace CardsBase.CLI;

internal class Menu
{
    public static void ShowMenu()
    {
        BlackJackManager blackJackManager = new(1, "Joel");
        blackJackManager.PlayGame();
    }
}

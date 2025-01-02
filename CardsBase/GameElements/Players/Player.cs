using CardsBase.GameElements.Collections;
using CardsBase.Logic;

namespace CardsBase.GameElements.Players;

public abstract class Player
{
    protected CardManager _cardManager;
    public Guid Id { get; }
    public string Name { get; }
    public bool IsAI { get; }
    public Player(CardManager manager, string name, bool isAI )
    {
        _cardManager = manager;
        Name = name;
        IsAI = isAI;
    }
    public abstract void ResetPlayer();
}

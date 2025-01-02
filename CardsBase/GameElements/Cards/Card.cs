using CardsBase.GameElements.Players;

namespace CardsBase.GameElements.Cards;

public abstract class Card
{
    private CardVisibility _visibility = CardVisibility.Hidden;
    private readonly HashSet<Player> _revealedTo = new(); // specific players
    public Guid Id { get; }
    public string Name { get; protected set; }

    public Card()
    {
        Id = Guid.NewGuid();
    }
    public override string ToString() => Name;
    internal bool IsRevealedTo(Player player)
    {
        if (_visibility == CardVisibility.Hidden) return false;
        if (_visibility == CardVisibility.Public) return true;
        return _revealedTo.Contains(player);
    }

    internal void RevealTo(Player player)
    {
        _visibility = CardVisibility.Private;
        _revealedTo.Add(player);
    }

    internal void Reveal()
    {
        _visibility = CardVisibility.Public;
    }

    internal void ResetVisibility()
    {
        _visibility = CardVisibility.Hidden;
        _revealedTo.Clear();
    }
}


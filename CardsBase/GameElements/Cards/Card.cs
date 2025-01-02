using CardsBase.GameElements.Collections;

namespace CardsBase.GameElements.Cards;

public abstract class Card
{
    public Guid Id { get; }
    public string Name { get; protected set; }
    public Card(string name)
    {
        Name = name;
        Id = Guid.NewGuid();
    }
    public CardCollection CurrentLocation { get; protected set; }
    public virtual void MoveTo(CardCollection newLocation)
    {
        if (newLocation == null) 
            throw new InvalidOperationException("Card cannot exist outside of a collection");
        CurrentLocation.RemoveCard(this);
        CurrentLocation = newLocation;
        newLocation.AddCard(this);
    }
}

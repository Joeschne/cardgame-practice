using CardsBase.GameElements.Cards;
using CardsBase.GameElements.Collections;

namespace CardsBase.Logic;

public class CardManager
{
    private readonly Dictionary<Guid, CardCollection> cardLocations = new();
    public void MoveCard(Card card, CardCollection newLocation)
    {
        if (card == null || newLocation == null)
            throw new InvalidOperationException("Card and target collection must not be null.");

        if (!cardLocations.TryGetValue(card.Id, out var currentLocation))
            throw new InvalidOperationException("Card is not currently in any collection.");

        // Remove from the current collection
        currentLocation.RemoveCard(card);

        // Add to the new collection
        newLocation.AddCard(card);

        // Update the location map
        cardLocations[card.Id] = newLocation;
    }
}

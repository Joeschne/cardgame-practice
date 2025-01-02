using CardsBase.GameElements.Cards;
using CardsBase.GameElements.Collections;

namespace CardsBase.Logic;

internal class CardManager
{
    private readonly Dictionary<Guid, CardCollection> cardLocations = new();

    internal void RegisterCard(Card card, CardCollection initialLocation)
    {
        cardLocations[card.Id] = initialLocation;
        initialLocation.AddCard(card);
    }
    internal void MoveCard(Card card, CardCollection newLocation, bool resetVisibility = true)
    {
        if (!cardLocations.TryGetValue(card.Id, out var currentLocation))
            throw new InvalidOperationException("Card is not currently in any collection.");

        if (currentLocation == newLocation)
            throw new InvalidOperationException("Card is already in the specified collection.");

        currentLocation.RemoveCard(card);
        newLocation.AddCard(card);

        // Reset visibility unless explicitly overridden
        if (resetVisibility)
            card.ResetVisibility();

        cardLocations[card.Id] = newLocation;
    }
}

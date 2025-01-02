using CardsBase.GameElements.Cards;
using CardsBase.GameElements.Collections;

namespace CardsBase.Logic;

public class CardManager
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

    internal void DrawCard(CardCollection origin, CardCollection destination, int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            if (origin.Cards.Count == 0)
                throw new InvalidOperationException("Origin is empty, card can't be drawn");
            Card drawnCard = origin.Cards[0];
            MoveCard(drawnCard, destination, false);
        }
    }
}

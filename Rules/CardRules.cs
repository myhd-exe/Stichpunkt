using System.Collections.Generic;
using System.Linq;
using Stichpunkt.Models;

namespace Stichpunkt.Rules;

public static class CardRules
{
    public static bool CanPlayCard(Card clickedCard, List<Card> playerHand, string? requiredColor)
    {
        if (requiredColor == null)
        {
            return true;
        }

        bool playerHasRequiredColor = playerHand.Any(card => card.Color == requiredColor);

        if (playerHasRequiredColor == false)
        {
            return true;
        }

        return clickedCard.Color == requiredColor || clickedCard.Color == "Neutral";
    }
}
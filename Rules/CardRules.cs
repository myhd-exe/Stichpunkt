using System.Collections.Generic;
using System.Linq;
using Stichpunkt.Models;

namespace Stichpunkt.Rules;

public class CardRules
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

    public static PlayedCard? allSameColor(List<PlayedCard> currentRound)
    {
        bool allSameColor = currentRound.All(played =>
        played.Card.Color == currentRound[0].Card.Color) && currentRound[0].Card.Color != "Neutral";

        if (allSameColor)
        {
            int highestValue = currentRound.Max(played => played.Card.Value);
            PlayedCard highestPlayedCard = currentRound.First(played => played.Card.Value == highestValue);

        PlayedCard highestCard = highestPlayedCard;
        return highestCard;
        }

        return null;
    }

    public static PlayedCard? DarknessandHero(List<PlayedCard> currentRound)
    {
       PlayedCard? darkness = currentRound.FirstOrDefault(PlayedCard => PlayedCard.Card.Value == 13);
       PlayedCard? hero = currentRound.FirstOrDefault(PlayedCard => PlayedCard.Card.Value == 0);

       if (darkness != null && hero != null)return hero;
       else if (darkness !=null && hero == null) return darkness;
        
        return null;
    }

    public static PlayedCard? MixColor(List<PlayedCard> currentRound)
    {

    if (currentRound.Count == 0)
        return null;

    int highestTrump = currentRound.Max(played => played.Card.TrumpOrder);

    return currentRound
        .Where(played => played.Card.TrumpOrder == highestTrump)
        .OrderByDescending(played => played.Card.Value)
        .FirstOrDefault(); 
    }
}
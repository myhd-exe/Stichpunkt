using System;
using System.Collections.Generic;

namespace Stichpunkt.Models;

public class PlayedCard
{
    public Card Card { get; set; }
    public string PlayerName { get; set; }
    public int PlayPosition { get; set; }

    public PlayedCard(Card card, string playerName, int playPosition)
    {
        Card = card;
        PlayerName = playerName;
        PlayPosition = playPosition;
    }
}
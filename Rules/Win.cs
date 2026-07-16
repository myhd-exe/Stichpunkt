using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Stichpunkt.Models;
using Avalonia.Input;
using System;
using System.Collections.Generic;
using Stichpunkt.Rules;
using System.Threading.Tasks;
namespace Stichpunkt.Rules;

public class Win
{
    public static PlayedCard? RoundWinner(List<PlayedCard> currentRound)
    {
        PlayedCard? winner;
        winner = CardRules.allSameColor(currentRound);
        if(winner == null)winner = CardRules.DarknessandHero(currentRound);
        if(winner == null)winner = CardRules.MixColor(currentRound);

        return winner;
    }
}
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Stichpunkt.Models;
using Avalonia.Input;
using System;
using System.Collections.Generic;
using Stichpunkt.Rules;

namespace Stichpunkt.Models;

public class Npcs
{
    public static Card BotChooseCard(List<Card> enemyHand, string? requiredColor)
    {
        List<Card> useableColor = new List<Card>();
        Random rdm = new Random();
        Card chooseCard;
        bool haveColor = false;
        if (requiredColor != null)
        {
            foreach (Card test in enemyHand)
            {
                if ((test.Color == requiredColor) ||  (test.Color == "Neutral") ) 
                {
                    haveColor = true;
                    useableColor.Add(test);
                
                };
            }
        }

        if (haveColor)
        {
             int rdmCard = rdm.Next(0,useableColor.Count);
             chooseCard = useableColor[rdmCard];
             enemyHand.Remove(chooseCard);
        }
        else
        {
            int rdmCard = rdm.Next(0,enemyHand.Count);
            chooseCard = enemyHand[rdmCard];
            enemyHand.Remove(chooseCard);
        }

        return chooseCard;
    }

    // public static Card BotPlayedCard(Card botchooseCard, )
    // {
    //     return playedCard;
    // }
}
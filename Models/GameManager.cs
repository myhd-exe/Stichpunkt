using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Stichpunkt.Views;

namespace Stichpunkt.Models
{
    public class GameManager
    {


        public static List<List<Card>> chooseOrder(DeckManager deckManager)
        {
            Random rdm = new Random();
            List<List<Card>> order = new List<List<Card>>();
            List<List<Card>> allPlayerHands = new List<List<Card>>
            {
                deckManager.PlayerHand,
                deckManager.Enemy1Hand,
                deckManager.Enemy2Hand,
                deckManager.Enemy3Hand
            };

            int rdmZahl = rdm.Next(0, allPlayerHands.Count);


            for (int i = 0; i < allPlayerHands.Count; i++)
            {
            int index = (rdmZahl + i) % allPlayerHands.Count; // Modulo Logik für Reihenfolge ausgabe! 3,0,1,2 bei z.B 2
            order.Add(allPlayerHands[index]);
            }
            return order;
        }

        public static Card? TurnRound(List<List<Card>>? order, out bool yourTurn, int currentTurnIndex, DeckManager deckManager,string? requiedColor)
        {
            yourTurn = false;

            if (order == null)
            {
                return null;
            }

            if (currentTurnIndex >= order.Count)
            {
                return null;
            }

            if (order[currentTurnIndex] == deckManager.PlayerHand)
            {
                yourTurn = true;
                return null;
            }

            Card playedCard = Npcs.BotChooseCard(order[currentTurnIndex], requiedColor);
            return playedCard;

        }
    }
}

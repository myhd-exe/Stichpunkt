using Avalonia;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Stichpunkt.Models
{
    public class Deck
    {
        public static List<Card> CreateDeck()
        {
            List<Card> deck = new List<Card>();
            string[] color = {"Orks","Daemonen","Menschen","Goblins","Neutral"};

            for (int i = 0; i< color.Length; i++)
            {
                if (i<4)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        deck.Add(new Card(color[i]+j,color[i],j+1,$"{color[i]}/{j+1}.png",i+1));
                    }
                }
                else
                {
                    for(int j = 0; j < 4; j++)
                    {
                        deck.Add(new Card(color[i]+"1."+j,color[i],0,$"{color[i]}/1.png",0));
                        deck.Add(new Card(color[i]+"2."+j,color[i],0,$"{color[i]}/2.png",5));
                    }
                }
            }

            return deck;
        }

        public static List<Card> StartHand(List<Card> deck)
        {
            List<Card> hand = new List<Card>();
            Random rdm = new Random();

            for (int i = 0;  i<7; i++)
            {
                int random = rdm.Next(0,deck.Count);
                hand.Add(deck[random]);
                deck.Remove(deck[random]);

            }

            return hand;
        }
        public static void DrawCard(List<Card> deck,List<Card> hand, int random)
        {
            deck.Remove(deck[random]);
            hand.Add(deck[random]);
        }
    }
}

using System.Collections.Generic;
using System;

namespace Stichpunkt.Models
{
public class DeckManager
{
    public List<Card> PlayerHand { get; private set; } = new();
    public List<Card> Enemy1Hand { get; private set; } = new();
    public List<Card> Enemy2Hand { get; private set; } = new();
    public List<Card> Enemy3Hand { get; private set; } = new();
    public List<Card> PlayDeck { get; private set; } = new();

    public List<Trumpf> Order { get; private set; } = new();

    public void DealStartHands()
    {
        CreateTrumpOrder();              // Order zuerst füllen

        PlayDeck = Deck.CreateDeck(Order);

        PlayerHand = Deck.StartHand(PlayDeck);
        Enemy1Hand = Deck.StartHand(PlayDeck);
        Enemy2Hand = Deck.StartHand(PlayDeck);
        Enemy3Hand = Deck.StartHand(PlayDeck);
    }

    public void CreateTrumpOrder()
    {
        Order.Clear(); // wichtig, falls die Methode mehrfach aufgerufen wird

        Random rdm = new Random();

        List<string> classes = new List<string>
        {
            "Mensch",
            "Goblin",
            "Daemon",
            "Ork"
        };

        for (int i = 0; i < 4; i++)
        {
            int rdmClass = rdm.Next(0, classes.Count);

            string temp = classes[rdmClass];

            Order.Add(new Trumpf(temp, i + 1, temp));

            classes.Remove(temp);
        }
    }

    public static List<Card> AllPlayedCards(Card player, Card enemy1, Card enemy2, Card enemy3)
    {
        List<Card> allPlayedCards = new List<Card>
        {
            player,
            enemy1,
            enemy2,
            enemy3
        };

        return allPlayedCards;
    }
}
}
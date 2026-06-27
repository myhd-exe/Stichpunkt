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
        public void DealStartHands()
        {
            PlayDeck = Deck.CreateDeck();
            PlayerHand = Deck.StartHand(PlayDeck);
            Enemy1Hand = Deck.StartHand(PlayDeck);
            Enemy2Hand = Deck.StartHand(PlayDeck);
            Enemy3Hand = Deck.StartHand(PlayDeck);
        }
    }
}
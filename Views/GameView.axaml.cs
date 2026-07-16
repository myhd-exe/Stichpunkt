using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Stichpunkt.Models;
using Avalonia.Input;
using System;
using System.Collections.Generic;
using Stichpunkt.Rules;
using System.Threading.Tasks;

namespace Stichpunkt.Views;

public partial class GameView : UserControl
{
    public DeckManager? deckManager;
    private Card? playerPlayedCard;
    bool yourTurn = true, playerCanPlay = false, newRound = false;
    string? requiredColor;
    int currentTurnIndex = 0, allCardPlayed = 0;
    PlayedCard? roundWinner;

    public List<PlayedCard> currentRound = new();

    private List<List<Card>>? order;


    public GameView()
    {
        InitializeComponent();
        deckManager = new DeckManager();
        ShowTrumpOrder(deckManager.Order);
        deckManager.DealStartHands();
        ShowPlayerHandCards(deckManager.PlayerHand);
        ShowEnemy1HandCards(deckManager.Enemy1Hand);
        ShowEnemy2HandCards(deckManager.Enemy2Hand);
        ShowEnemy3HandCards(deckManager.Enemy3Hand);
        order = GameManager.chooseOrder(deckManager);

        //Runden Start
        ContinueTurn();

    }


    private void NewGame()
    {
        currentTurnIndex = 0;
        deckManager = new DeckManager();
        ShowTrumpOrder(deckManager.Order);
        deckManager.DealStartHands();
        ShowPlayerHandCards(deckManager.PlayerHand);
        ShowEnemy1HandCards(deckManager.Enemy1Hand);
        ShowEnemy2HandCards(deckManager.Enemy2Hand);
        ShowEnemy3HandCards(deckManager.Enemy3Hand);
        order = GameManager.chooseOrder(deckManager);
        ContinueTurn();
    }






    //KartenZug
    private async void ContinueTurn()
    {
        while (currentTurnIndex < order.Count)
        {
            Console.WriteLine("---- Schleifendurchlauf ----");
            Console.WriteLine($"currentTurnIndex: {currentTurnIndex}");
            Console.WriteLine($"order[currentTurnIndex]: {order[currentTurnIndex]}");

            Card? playedCard = GameManager.TurnRound(
                order,
                out yourTurn,
                currentTurnIndex,
                deckManager,
                requiredColor
            );

            Console.WriteLine($"yourTurn: {yourTurn}");
            Console.WriteLine($"playedCard == null: {playedCard == null}");

            if (playedCard != null)
            {
                Console.WriteLine($"playedCard: {playedCard.Name} | {playedCard.Color} | {playedCard.Value}");
            }

            if (yourTurn)
            {
                Console.WriteLine("STOP: Spieler ist dran");
                playerCanPlay = true;

                string fullPath = "avares://Stichpunkt/Assets/deinZug.png";
                ActionField.Source = new Bitmap(AssetLoader.Open(new Uri(fullPath)));
                ActionField.IsVisible = true;

                return;
            }

            if (playedCard == null)
            {
                Console.WriteLine("STOP: playedCard ist null");
                return;
            }

            Console.WriteLine("Vor EnemyCardPlay");

            if (currentTurnIndex != 0)
                await Task.Delay(1000);

            EnemyCardPlay(order, currentTurnIndex, deckManager, playedCard);

            Console.WriteLine("Nach EnemyCardPlay");

            if (requiredColor == null && playedCard.Color != "Neutral")
            {
                requiredColor = playedCard.Color;
                Console.WriteLine($"requiredColor gesetzt: {requiredColor}");
            }

            Console.WriteLine($"Aktuelle currentRound.Count: {currentRound.Count}");

            if (CheckRoundEnd())
            {
                return;
            }


            currentTurnIndex++;

            Console.WriteLine($"currentTurnIndex nach ++: {currentTurnIndex}");
        }

    }
    //Methode für Kartenauswahl
    private void HandCard_Tapped(object? sender, TappedEventArgs e)
    {
        if (playerCanPlay == true)
        {



            Border? clickedBorder = sender as Border;

            if (clickedBorder == null)
                return;

            int handIndex = int.Parse(clickedBorder.Tag!.ToString()!);

            Image[] handImages =
            {
            HandCard0,
            HandCard1,
            HandCard2,
            HandCard3,
            HandCard4,
            HandCard5,
            HandCard6
    };

            Console.WriteLine($"Index: {handIndex}");
            Console.WriteLine($"Handkarten: {deckManager.PlayerHand.Count}");
            if (handIndex < 0 || handIndex >= deckManager.PlayerHand.Count)
                {
                Console.WriteLine($"Ungültiger Index: {handIndex}, Count: {deckManager.PlayerHand.Count}");
                return;
                }
            Card playedCard = deckManager.PlayerHand[handIndex];

            bool canPlay = CardRules.CanPlayCard(
                playedCard,
                deckManager.PlayerHand,
                requiredColor);

            if (canPlay == false)
            {
                return;
            }

            playerPlayedCard = playedCard;

            currentRound.Add(new PlayedCard(playerPlayedCard, "Player", currentRound.Count));

            PlayedCardPlayer.Source = handImages[handIndex].Source;

            if (requiredColor == null && playedCard.Color != "Neutral")
            {
                requiredColor = playedCard.Color;
            }

            deckManager.PlayerHand.RemoveAt(handIndex);

            ShowPlayerHandCards(deckManager.PlayerHand);
            if (CheckRoundEnd())
            {
                return;
            }

            if(currentTurnIndex != 4) currentTurnIndex++;
            playerCanPlay = false;
            ActionField.Source = null;
            ActionField.IsVisible = false;
            ContinueTurn();
        }
        else return;
    }
    //Methode zum anzeigen und zordnen der Gespieleten KI Karte
    private void EnemyCardPlay(List<List<Card>> Order, int currentTurnIndex, DeckManager deckManager, Card playedCard)
    {
        string path = $"avares://Stichpunkt/Assets/Cards/{playedCard.ImagePath}";

        if (Order[currentTurnIndex] == deckManager.Enemy1Hand)
        {
            currentRound.Add(new PlayedCard(playedCard, "Enemy1", currentRound.Count));
            PlayedEnemy1.Source = new Bitmap(AssetLoader.Open(new Uri(path)));
            ShowEnemy1HandCards(Order[currentTurnIndex]);
        }
        else if (Order[currentTurnIndex] == deckManager.Enemy2Hand)
        {
            currentRound.Add(new PlayedCard(playedCard, "Enemy2", currentRound.Count));
            PlayedEnemy2.Source = new Bitmap(AssetLoader.Open(new Uri(path)));
            ShowEnemy2HandCards(Order[currentTurnIndex]);
        }
        else if (Order[currentTurnIndex] == deckManager.Enemy3Hand)
        {
            currentRound.Add(new PlayedCard(playedCard, "Enemy3", currentRound.Count));
            PlayedEnemy3.Source = new Bitmap(AssetLoader.Open(new Uri(path)));
            ShowEnemy3HandCards(Order[currentTurnIndex]);
        }
    }

    // Anzeige der Hände und Trumpf Folge
    private void ShowPlayerHandCards(List<Card> handCards)
    {
        Image[] imageFieldsPlayer =
        {
    HandCard0,
    HandCard1,
    HandCard2,
    HandCard3,
    HandCard4,
    HandCard5,
    HandCard6
    };
        foreach (Image image in imageFieldsPlayer)
        {
            image.Source = null;
        }
        for (int i = 0; i < handCards.Count && i < imageFieldsPlayer.Length; i++)
        {
            string fullPath = $"avares://Stichpunkt/Assets/Cards/{handCards[i].ImagePath}";
            imageFieldsPlayer[i].Source = new Bitmap(AssetLoader.Open(new Uri(fullPath)));
        }
    }
    private void ShowEnemy1HandCards(List<Card> enemy1Cards)
    {
        Image[] imageFieldsPlayer =
        {
    EnemyHandCard0,
    EnemyHandCard1,
    EnemyHandCard2,
    EnemyHandCard3,
    EnemyHandCard4,
    EnemyHandCard5,
    EnemyHandCard6,
    };
        foreach (Image image in imageFieldsPlayer)
        {
            image.Source = null;
        }
        for (int i = 0; i < enemy1Cards.Count && i < imageFieldsPlayer.Length; i++)
        {
            string fullPath = $"avares://Stichpunkt/Assets/Cards/back.png";

            imageFieldsPlayer[i].Source = new Bitmap(AssetLoader.Open(new Uri(fullPath)));
        }
    }
    private void ShowEnemy2HandCards(List<Card> enemy2Cards)
    {
        Image[] imageFieldsPlayer =
        {
        EnemyHandCard7,
        EnemyHandCard8,
        EnemyHandCard9,
        EnemyHandCard10,
        EnemyHandCard11,
        EnemyHandCard12,
        EnemyHandCard13,
        };

        foreach (Image image in imageFieldsPlayer)
        {
            image.Source = null;
        }
        for (int i = 0; i < enemy2Cards.Count && i < imageFieldsPlayer.Length; i++)
        {
            string fullPath = $"avares://Stichpunkt/Assets/Cards/back.png";

            imageFieldsPlayer[i].Source = new Bitmap(AssetLoader.Open(new Uri(fullPath)));
        }
    }
    private void ShowEnemy3HandCards(List<Card> enemy1Cards)
    {
        Image[] imageFieldsPlayer =
        {
        EnemyHandCard14,
        EnemyHandCard15,
        EnemyHandCard16,
        EnemyHandCard17,
        EnemyHandCard18,
        EnemyHandCard19,
        EnemyHandCard20,
        };
        foreach (Image image in imageFieldsPlayer)
        {
            image.Source = null;
        }
        for (int i = 0; i < enemy1Cards.Count && i < imageFieldsPlayer.Length; i++)
        {
            string fullPath = $"avares://Stichpunkt/Assets/Cards/back.png";

            imageFieldsPlayer[i].Source = new Bitmap(AssetLoader.Open(new Uri(fullPath)));
        }
    }
    private void ShowTrumpOrder(List<Trumpf> TrumpOrder)
    {
        Image[] TrumpShow =
        {
        Trumpf1,
        Trumpf2,
        Trumpf3,
        Trumpf4
        };
        for (int i = 0; i < TrumpOrder.Count && i < TrumpShow.Length; i++)
        {
            string fullPath = $"avares://Stichpunkt/Assets/Trumpf/{TrumpOrder[i].ImagePath}.png";

            TrumpShow[i].Source = new Bitmap(AssetLoader.Open(new Uri(fullPath)));
        }

    }
    // Sieger Anzeigen
    private void ShowRoundWinner(PlayedCard roundWinner)
    {
        if (roundWinner.PlayerName == "Player")
        {
            string fullPath = "avares://Stichpunkt/Assets/RoundWin/0.png";

            ActionField.Source = new Bitmap(AssetLoader.Open(new Uri(fullPath)));
            ActionField.IsVisible = true;
        }
        else if (roundWinner.PlayerName == "Enemy1")
        {
            string fullPath = "avares://Stichpunkt/Assets/RoundWin/1.png";

            ActionField.Source = new Bitmap(AssetLoader.Open(new Uri(fullPath)));
            ActionField.IsVisible = true;
        }
        else if (roundWinner.PlayerName == "Enemy2")
        {
            string fullPath = "avares://Stichpunkt/Assets/RoundWin/2.png";

            ActionField.Source = new Bitmap(AssetLoader.Open(new Uri(fullPath)));
            ActionField.IsVisible = true;
        }
        else if (roundWinner.PlayerName == "Enemy3")
        {
            string fullPath = "avares://Stichpunkt/Assets/RoundWin/3.png";

            ActionField.Source = new Bitmap(AssetLoader.Open(new Uri(fullPath)));
            ActionField.IsVisible = true;
        }
    }

    private bool CheckRoundEnd()
    {
        if (currentRound.Count == 4)
        {
            foreach (var played in currentRound)
            {
                Console.WriteLine($"{played.PlayerName}: {played.Card.Name} | {played.Card.Color} | {played.Card.Value}| {played.Card.TrumpOrder}");
            }

            roundWinner = Win.RoundWinner(currentRound);

            if (roundWinner != null)
            {
                ShowRoundWinner(roundWinner);
                nextRound.IsVisible = true;
                nextRound.IsHitTestVisible = true;
                newRound = true;
                allCardPlayed++;
            }
            else
            {
                string fullPath = "avares://Stichpunkt/Assets/naechsteRunde.png";

                ActionField.Source = new Bitmap(AssetLoader.Open(new Uri(fullPath)));
                ActionField.IsVisible = true;
            }

            return true;
        }

        return false;
    }

    private void NextRound_Tapped(object? sender, TappedEventArgs e)
    {
        if (newRound)
        {
            currentTurnIndex = 0;
            requiredColor = null;
            currentRound.Clear();
            newRound = false;
            playerCanPlay = false;

            PlayedEnemy1.Source = null;
            PlayedEnemy2.Source = null;
            PlayedEnemy3.Source = null;
            PlayedCardPlayer.Source = null;

            ActionField.IsVisible = false;
            nextRound.IsVisible = false;

            if (allCardPlayed <= 6) ContinueTurn();
            else
            {
                allCardPlayed = 0;
                NewGame();
            }
        }

    }
}
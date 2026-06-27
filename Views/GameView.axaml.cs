using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Stichpunkt.Models;
using Avalonia.Input;
using System;
using System.Collections.Generic;
using Stichpunkt.Rules;

namespace Stichpunkt.Views;

public partial class GameView : UserControl
{
    public DeckManager? deckManager;
    private Card? playerPlayedCard;
    bool yourTurn = true;
    string?  requiredColor;
    int currentTurnIndex = 0;

    private Card? playedEnemy1Card;
    private Card? playedEnemy2Card;
    private Card? playedEnemy3Card;
    private Card? yourPlayedCard;

    private List<List<Card>>? order;
    bool playerCanPlay = false;


    public GameView()
    {
        InitializeComponent();
        deckManager = new DeckManager();
        
        ShowTrumpOrder(GameManager.TrumpOrder());
        deckManager.DealStartHands();
        ShowPlayerHandCards(deckManager.PlayerHand);
        ShowEnemy1HandCards(deckManager.Enemy1Hand);
        ShowEnemy2HandCards(deckManager.Enemy2Hand);
        ShowEnemy3HandCards(deckManager.Enemy3Hand);
        order = GameManager.chooseOrder(deckManager);

        //Runden Start
        ContinueTurn();
    }








        
//KartenZug
private void ContinueTurn()
{
    while (currentTurnIndex < order.Count)
{
    Card? playedCard = GameManager.TurnRound(order,out yourTurn,currentTurnIndex,deckManager,requiredColor);

    if (yourTurn)
    {
        playerCanPlay = true;
        return;
    }

    if (playedCard == null)
    {
        return;
    }

    EnemyCardPlay(order, currentTurnIndex, deckManager, playedCard);

    if (requiredColor == null)
    {
        requiredColor = playedCard.Color;
    }

    currentTurnIndex++;
}

}
//Methode für Kartenauswahl
private void HandCard_Tapped(object? sender, TappedEventArgs e)
{
    if(playerCanPlay == true)
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

    Card playedCard = deckManager.PlayerHand[handIndex];

bool canPlay = CardRules.CanPlayCard(
    playedCard,
    deckManager.PlayerHand,
    requiredColor );

if (canPlay == false)
{
    return;
}

    playerPlayedCard = playedCard;

    yourPlayedCard = playedCard;

    PlayedCardPlayer.Source = handImages[handIndex].Source;
    
    if (requiredColor == null)
        {
        requiredColor = playedCard.Color;
        }

    deckManager.PlayerHand.RemoveAt(handIndex);

    ShowPlayerHandCards(deckManager.PlayerHand);

    currentTurnIndex++;
    playerCanPlay = false;
    ContinueTurn();
}
else return;
}

private void EnemyCardPlay(List<List<Card>> Order, int currentTurnIndex,DeckManager deckManager, Card playedCard)
    {
        string path = $"avares://Stichpunkt/Assets/Cards/{playedCard.ImagePath}";
        
        if (Order[currentTurnIndex] == deckManager.Enemy1Hand)
        {
            playedEnemy1Card = playedCard;
            PlayedEnemy1.Source = new Bitmap(AssetLoader.Open(new Uri(path)));
            ShowEnemy1HandCards(Order[currentTurnIndex]);
        }
        else if (Order[currentTurnIndex] == deckManager.Enemy2Hand)
        {
            playedEnemy2Card = playedCard;
            PlayedEnemy2.Source = new Bitmap(AssetLoader.Open(new Uri(path)));
            ShowEnemy2HandCards(Order[currentTurnIndex]);
        }
        else if (Order[currentTurnIndex] == deckManager.Enemy3Hand)
        {
            playedEnemy3Card = playedCard;
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
}
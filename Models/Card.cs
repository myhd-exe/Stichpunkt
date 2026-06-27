using Avalonia;
using System;

namespace Stichpunkt.Models
{
    public class Card
    {
    public string Name { get; private set; }
    public string Color{ get; private set; }
    public int Value { get; private set; }
    public string ImagePath { get; private set; }
    public int TrumpOrder {get; private set;}

    public Card(string name, string color, int value, string image, int order)
        {
            Name = name;
            Color = color;
            Value = value;
            ImagePath = image;
            TrumpOrder = order;
        }
    }
}


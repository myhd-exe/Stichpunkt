using System;
using System.Collections.Generic;
using System.Text;

namespace Stichpunkt.Models
{
    internal class Trumpf
    {
        public string Classname { get; private set; }
        public int TrumpfNumber { get; private set; }
        public string ImagePath { get; private set; }


        public Trumpf(string name, int trNr, string image)
        {
            Classname = name;
            TrumpfNumber = trNr;
            ImagePath = image;
        }
    }
}

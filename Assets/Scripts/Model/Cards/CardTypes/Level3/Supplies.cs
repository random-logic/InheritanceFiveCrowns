using System.Collections;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class Supplies : Card
    {
        public Supplies(App app) : base(app)
        {
            Title = "Supplies";
            BaseLevel = 3;
            Description = "Draw 2 cards.";
            OnEnactOnPlayedPowers.Add(new DrawCards(2));
        }
    }
}
using System.Collections;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class Library : Card
    {
        public Library(App app) : base(app)
        {
            Title = "Library";
            BaseLevel = 2;
            Description = "Draw until you have 4 cards in your hand.";
            OnEnactOnPlayedPowers.Add(new DrawCardsUntil(4));
        }
    }
}
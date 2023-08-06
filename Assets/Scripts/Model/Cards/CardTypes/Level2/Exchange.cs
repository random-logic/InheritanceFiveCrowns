using System.Collections;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class Exchange : Card
    {
        public Exchange(App app) : base(app)
        {
            Title = "Exchange";
            BaseLevel = 2;
            Description = "Draw a card. Discard any number of cards, then draw that many cards.";
            OnEnactOnPlayedPowers.Add(new DrawCards());
            OnEnactOnPlayedPowers.Add(new ExchangeCards());
        }
    }
}
using System.Collections;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class Lighthouse : Card
    {
        public Lighthouse(App app) : base(app)
        {
            Title = "Lighthouse";
            BaseLevel = 2;
            Description = "If you have less Vp than your opponent when you play this, worth 2 Vp.";
            OnEnactOnPlayedPowers.Add(new MoveIfLessVpThanNextPlayer(new AddVpToCard(2, this)));
        }
    }
}
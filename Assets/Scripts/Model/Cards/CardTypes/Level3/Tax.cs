using System.Collections;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class Tax : Card
    {
        public Tax(App app) : base(app)
        {
            Title = "Tax";
            BaseLevel = 3;
            Description = "Your opponent decides: This is worth 4 Vp or discard randomly until 4 cards in your hand.";
            OnEnactOnPlayedPowers.Add(new MoveOnNextPlayerStartTurn(
                new MoveChoice("Opponent gets 4 Vp OR randomly discard cards until you have 4", 
                "Opponent Gets Vp", new AddVpToCard(4, this),
                "Discard Cards", new RandomlyDiscardCardsUntil(4))));
        }
    }
}
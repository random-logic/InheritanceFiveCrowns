using System.Collections;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class BlackMarket : Card
    {
        public BlackMarket(App app) : base(app)
        {
            Title = "Black_Market";
            Description = "Put any card from the discard pile into your hand";
            OnEnactOnPlayedPowers.Add(new PromptToPutDiscardedCardsIntoHand());
        }
    }
}
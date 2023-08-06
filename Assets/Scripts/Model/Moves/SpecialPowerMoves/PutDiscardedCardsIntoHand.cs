using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class PutDiscardedCardsIntoHand : Move
    {
        protected List<Card> Cards;

        public PutDiscardedCardsIntoHand(List<Card> cards)
        {
            Cards = cards;
        }

        protected internal override Task<int> Enact()
        {
            Model.DiscardPile.Remove(Cards);
            Model.PlayerTracker.GetMoveMaker().AddCardsToHand(Cards);

            return Task.FromResult(0);
        }
    }
}
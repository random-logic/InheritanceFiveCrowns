using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class PutPlayedCardsIntoHand : Move
    {
        protected List<Card> Cards;

        public PutPlayedCardsIntoHand(List<Card> cards)
        {
            Cards = cards;
        }
        protected internal override Task<int> Enact()
        {
            Player player = Model.PlayerTracker.GetMoveMaker();
            player.RemoveCardsFromPlay(Cards);
            player.AddCardsToHand(Cards);
            return Task.FromResult(0);
        }
    }
}
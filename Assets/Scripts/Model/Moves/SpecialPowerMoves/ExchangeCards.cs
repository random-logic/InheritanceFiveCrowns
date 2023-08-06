using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class ExchangeCards : Move
    {
        protected internal override async Task<int> Enact()
        {
            Player moveMaker = Model.PlayerTracker.GetMoveMaker();
            await moveMaker.OptionallyChooseCardsToDiscard(moveMaker.Hand.Count);
            moveMaker.DrawCards(moveMaker.NumberOfCardsDiscardedPreviously);
            return 0;
        }
    }
}
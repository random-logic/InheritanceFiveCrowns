using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class DrawCardsUntil : Move
    {
        protected int TargetCount;

        public DrawCardsUntil(int targetCount)
        {
            TargetCount = targetCount;
        }

        protected internal override Task<int> Enact()
        {
            Player moveMaker = Model.PlayerTracker.GetMoveMaker();
            int handCount = moveMaker.Hand.Count;
            if (handCount < TargetCount)
                moveMaker.DrawCards(TargetCount - handCount);
            return Task.FromResult(0);
        }
    }
}
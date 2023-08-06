using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using static InheritanceFiveCrowns.Utility;

namespace InheritanceFiveCrowns
{
    public class RandomlyDiscardCardsUntil : Move
    {
        protected int TargetCount;

        public RandomlyDiscardCardsUntil(int targetCount)
        {
            TargetCount = targetCount;
        }

        protected internal override Task<int> Enact()
        {
            Player moveMaker = Model.PlayerTracker.GetMoveMaker();
            while (moveMaker.Hand.Count > TargetCount)
                RemoveRandomElement(moveMaker.Hand.Container);
            return Task.FromResult(0);
        }
    }
}
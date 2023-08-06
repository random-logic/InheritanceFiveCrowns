using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class PromptToPutDiscardedCardsIntoHand : Move
    {
        protected int Count;

        public PromptToPutDiscardedCardsIntoHand(int count = 1)
        {
            Count = count;
        }

        protected internal override Task<int> Enact()
            => Model.PlayerTracker.GetMoveMaker().ChooseDiscardedCardsToPutIntoHand(Count);
    }
}
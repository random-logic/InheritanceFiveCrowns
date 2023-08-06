using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class PromptToPutPlayedCardsIntoHand : Move
    {
        protected int Count;

        public PromptToPutPlayedCardsIntoHand(int count = 1)
        {
            Count = count;
        }

        protected internal override Task<int> Enact()
            => Model.PlayerTracker.GetMoveMaker().ChoosePlayedCardsToPutIntoHand(Count);
    }
}
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class RemoveLevelModifier : Move
    {
        protected Card Card;
        protected int Count;

        public RemoveLevelModifier(Card card, int count = 1)
        {
            Card = card;
            Count = count;
        }

        protected internal override Task<int> Enact()
        {
            Card.ControlledPlayer.LevelModifier -= Count;
            return Task.FromResult(0);
        }
    }
}
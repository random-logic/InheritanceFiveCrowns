using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    // This should be an on going power for a card
    public class AddLevelModifier : Move
    {
        protected Card Card;
        protected int Count;

        public AddLevelModifier(Card card, int count = 1)
        {
            Card = card;
            Count = count;
        }

        protected internal override Task<int> Enact()
        {
            Card.ControlledPlayer.LevelModifier += Count;
            return Task.FromResult(0);
        }
    }
}
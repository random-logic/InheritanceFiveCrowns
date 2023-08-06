using System.Collections;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class VpPerControlledCard : VpCalculation
    {
        // For cards like mining
        protected Card Card;
        protected int Multiplier;

        public VpPerControlledCard(Card card, int multiplier = 1)
        {
            Card = card;
            Multiplier = multiplier;
        }

        public override int GetVp()
            => Card.ControlledPlayer.Controlled.Count * Multiplier;
    }
}
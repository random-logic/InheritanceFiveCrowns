using System.Collections;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class VpPerSubCard : VpCalculation
    {
        // For cards like banking
        protected Card Card;
        protected int Multiplier;

        public VpPerSubCard(Card card, int multiplier = 1)
        {
            Card = card;
            Multiplier = multiplier;
        }

        public override int GetVp()
            => Card.SubCards.Count * Multiplier;
    }
}
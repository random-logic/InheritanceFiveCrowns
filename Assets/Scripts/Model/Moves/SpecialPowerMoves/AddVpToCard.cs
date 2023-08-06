using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class AddVpToCard : Move
    {
        protected int Vp;
        protected Card Card;

        public AddVpToCard(int vp, Card card)
        {
            Vp = vp;
            Card = card;
        }

        protected internal override Task<int> Enact()
        {
            Card.AdditionalFixedVp += Vp;
            return Task.FromResult(0);
        }
    }
}
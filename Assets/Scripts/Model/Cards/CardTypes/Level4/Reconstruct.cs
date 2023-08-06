using System.Collections;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class Reconstruct : Card
    {
        public Reconstruct(App app) : base(app)
        {
            Title = "Reconstruct";
            Description = "Return another card in play and any cards on it to its controller's hand.";
            OnEnactOnPlayedPowers.Add(new PromptToPutPlayedCardsIntoHand());
        }
    }
}
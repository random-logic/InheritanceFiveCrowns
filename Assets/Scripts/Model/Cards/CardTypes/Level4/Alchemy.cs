using System.Collections;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class Alchemy : Card
    {
        public Alchemy(App app) : base(app)
        {
            Title = "Alchemy";
            Description = "Discard 2 cards.";
            BaseVp = 4;
            OnEnactOnPlayedPowers.Add(new PromptToDiscardCards(2));
        }
    }
}
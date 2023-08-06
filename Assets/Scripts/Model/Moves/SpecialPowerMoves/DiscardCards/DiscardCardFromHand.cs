using System.Collections.Generic;
using System.Threading.Tasks;

namespace InheritanceFiveCrowns {
    public class DiscardCardFromHand : Move {
        public List<Card> Cards;

        public DiscardCardFromHand(List<Card> cards)
        {
            Cards = cards;
        }

        protected internal override Task<int> Enact()
        {
            Model.PlayerTracker.GetMoveMaker().DiscardCardsFromHand(Cards);
            return Task.FromResult(0);
        }
    }
}
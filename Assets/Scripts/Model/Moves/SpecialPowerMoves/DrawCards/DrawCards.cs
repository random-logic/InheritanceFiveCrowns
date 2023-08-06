using System.Collections.Generic;
using System.Threading.Tasks;

namespace InheritanceFiveCrowns {
    public class DrawCards : Move {
        // The move maker draws cards
        protected int Count;

        public DrawCards(int count = 1) {
            Count = count;
        }

        protected internal override Task<int> Enact() {
            Model.PlayerTracker.GetMoveMaker().DrawCards(Count);
            return Task.FromResult(0);
        }
    }
}
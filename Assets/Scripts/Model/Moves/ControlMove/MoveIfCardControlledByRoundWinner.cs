using System.Threading.Tasks;

namespace InheritanceFiveCrowns {
    public class MoveIfCardControlledByRoundWinner : Move {
        protected Move Move;
        protected Card Card;
        
        public MoveIfCardControlledByRoundWinner(Move move, Card card) {
            Move = move;
            Card = card;
        }

        protected internal override async Task<int> Enact() {
            if (Card.ControlledPlayer == Model.DeterminedRoundWinner)
                await Model.MakeMove(Move);
            return 0;
        }
    }
}
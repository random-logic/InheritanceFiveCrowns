using System.Threading.Tasks;

namespace InheritanceFiveCrowns {
    public class MoveOnEndTurn : Move {
        protected Move Move;
        
        public MoveOnEndTurn(Move move)
        {
            Move = move;
        }

        protected internal override Task<int> Enact() {
            Player player = Model.PlayerTracker.GetMoveMaker();
            player.MovesOnEndTurn.Add(Move);
            return Task.FromResult(0);
        }
    }
}
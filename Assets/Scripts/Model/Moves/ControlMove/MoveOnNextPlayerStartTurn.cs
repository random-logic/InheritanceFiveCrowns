using System.Threading.Tasks;
using System;

namespace InheritanceFiveCrowns
{
    public class MoveOnNextPlayerStartTurn : Move
    {
        protected Move Move;

        public MoveOnNextPlayerStartTurn(Move move)
        {
            Move = move;
        }

        protected internal override Task<int> Enact()
        {
            Model.PlayerTracker.GetNextPlayer().MovesOnStartTurn.Add(Move);
            return Task.FromResult(0);
        }
    }
}
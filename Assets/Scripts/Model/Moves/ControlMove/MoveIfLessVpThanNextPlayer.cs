using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class MoveIfLessVpThanNextPlayer : Move
    {
        protected Move Move;

        public MoveIfLessVpThanNextPlayer(Move move)
        {
            Move = move;
        }

        protected internal override async Task<int> Enact()
        {
            PlayerTracker tracker = Model.PlayerTracker;
            Player moveMaker = tracker.GetMoveMaker();
            Player next = tracker.GetNextPlayer();

            if (moveMaker.Vp < next.Vp)
                await Model.MakeMove(Move);

            return 0;
        }
    }
}
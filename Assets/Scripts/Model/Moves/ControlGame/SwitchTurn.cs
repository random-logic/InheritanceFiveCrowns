using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InheritanceFiveCrowns {
    public class SwitchTurn : Move {
        protected internal override async Task<int> Enact() {
            PlayerTracker playerTracker = Model.PlayerTracker;
            await playerTracker.GetMoveMaker().OnEndTurn();
            playerTracker.SwitchTurns();
            await playerTracker.GetMoveMaker().OnSwitchTurn();
            await playerTracker.GetMoveMaker().OnStartTurn();
            return 0;
        }
    }
}
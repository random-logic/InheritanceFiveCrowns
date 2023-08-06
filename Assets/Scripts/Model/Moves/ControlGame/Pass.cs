using System.Threading.Tasks;

namespace InheritanceFiveCrowns {
    public class Pass : Move
    {
        protected bool AutoSwitchTurn;

        // If auto end turn is disabled, the turn has to be switched manually by the user via user input
        public Pass(bool autoEndTurn)
        {
            AutoSwitchTurn = autoEndTurn;
        }

        protected internal override async Task<int> Enact()
        {
            Model.PlayerTracker.GetMoveMaker().Pass();
            if (Model.PassCounter.PassQuotaReached())
                Model.EndRound();
            else if (AutoSwitchTurn)
                await Model.MakeMove(new SwitchTurn());

            return 0;
        }
    }
}
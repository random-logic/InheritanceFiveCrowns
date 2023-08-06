using System.Threading.Tasks;

namespace InheritanceFiveCrowns {
    public class PlayCard : Move
    {
        protected Card Card;
        protected bool AutoSwitchTurn;

        public PlayCard(Card card, bool autoSwitchTurn = false)
        {
            Card = card;
            AutoSwitchTurn = autoSwitchTurn;
        }

        protected internal override async Task<int> Enact() {
            await Model.PlayerTracker.GetMoveMaker().PlayCard(Card, AutoSwitchTurn);
            return 0;
        }
    }
}

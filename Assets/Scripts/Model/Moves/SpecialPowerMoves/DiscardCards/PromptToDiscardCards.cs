using System.Threading.Tasks;

namespace InheritanceFiveCrowns
{
    public class PromptToDiscardCards : Move
    {
        protected int Count;

        public PromptToDiscardCards(int count = 1)
        {
            Count = count;
        }

        protected internal override async Task<int> Enact()
        {
            await Model.PlayerTracker.GetMoveMaker().ChooseCardsToDiscard(Count);
            return 0;
        }
    }
}
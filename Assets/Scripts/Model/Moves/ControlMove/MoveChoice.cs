using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class MoveChoice : Move
    {
        protected string Prompt, Choice0, Choice1;
        protected Move Move0, Move1;

        public MoveChoice(string prompt, string choice0, Move move0, string choice1, Move move1)
        {
            Prompt = prompt;
            Choice0 = choice0;
            Choice1 = choice1;
            Move0 = move0;
            Move1 = move1;
        }

        protected internal override async Task<int> Enact()
        {
            int choice = await View.PromptChoice(Prompt, Choice0, Choice1);
            if (choice == 0)
                await Model.MakeMove(Move0);
            else 
                await Model.MakeMove(Move1);
            return 0;
        }
    }
}
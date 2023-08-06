using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class View : ViewObject
    {
        protected App App;
        protected Controller Controller => App.Controller;

        public TapToContinue TapToContinue { protected set; get; }
        public Choice Choice { protected set; get; }
        
        public TopPanel TopPanel { protected set; get; }

        public DisplayCards DisplayCards { protected set; get; }

        public DiscardPileView DiscardPileView => DisplayCards.DiscardPileView;
        public OpponentControlledCards OpponentControlledCards => DisplayCards.OpponentControlledCards;
        public YourControlledCards YourControlledCards => DisplayCards.YourControlledCards;

        public PlayerChoices PlayerChoices { protected set; get; }
        public PlayerHand PlayerHand { protected set; get; }

        public Action<int, int> SetPasses => TopPanel.PassesView.Set;
        public Action<int, int> SetCrowns => TopPanel.CrownsView.Set;

        public Func<string, Task<int>> PromptMessage => TapToContinue.Prompt;
        public Func<string, string, string, Task<int>> PromptChoice => Choice.Prompt;

        public Action<ModelList<Card>> OnUpdateDiscardPile => DiscardPileView.UpdateView;

        public Func<int, Task<int>> PromptToOptionallyDiscardCards =>
            PlayerHand.OptionallyDiscardCardsFromHand;
        public Func<int, Task<int>> PromptToDiscardCards => PlayerHand.DiscardCardsFromHand;

        public Action<Player> UpdateHand => PlayerHand.UpdateView;
        public Action<Player> UpdateYourControlledCards => YourControlledCards.UpdateView;
        public Action<Player> UpdateOpponentControlledCards => OpponentControlledCards.UpdateView;

        public Func<int, Task<int>> PromptUserToPutDiscardedCardsIntoHand => DiscardPileView.PutDiscardedCardsIntoHand;
        public Func<int, Task<int>> PromptUserToPutPlayedCardsIntoHand => YourControlledCards.PutPlayedCardsIntoHand;

        public MakeMoveButton MakeMoveButton => PlayerChoices.MakeMoveButton;

        public void Awake()
        {
            App = App.Get();
            TapToContinue = GetComponentInChildren<TapToContinue>(true);
            Choice = GetComponentInChildren<Choice>(true);
            DisplayCards = GetComponentInChildren<DisplayCards>(true);
            PlayerChoices = GetComponentInChildren<PlayerChoices>(true);
            PlayerHand = GetComponentInChildren<PlayerHand>(true);
        }

        public async Task<int> OnDeterminedRoundWinner(bool existsWinner, string winner)
        {
            if (existsWinner) await PromptMessage(winner + " won the round");
            else await PromptMessage("No one won this round");
            return 0;
        }

        public async Task<int> OnSwitchTurn(Player moveMaker, Player opponent)
        {
            await PromptMessage("Pass the device to " + moveMaker.Name);
            UpdateHand(moveMaker);
            UpdateYourControlledCards(moveMaker);
            UpdateOpponentControlledCards(opponent);
            return 0;
        }

        public Task<int> OnStartTurnCompleted(Player moveMaker)
        {
            YourControlledCards.AddPlayCardSlot();
            MakeMoveButton.PassWhenPressed();
            return Task.FromResult(0);
        }
    }
}
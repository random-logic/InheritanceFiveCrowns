using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class Controller : MonoBehaviour
    {
        // This is the place to check for cheating when hosting game on servers?
        // Maybe put this in the same assembly as the model

        public enum Status
        {
            ViewingPlayArea, ChoosingCardsToDiscard, ChoosingOption
        }

        public Status CurrentStatus;

        protected App App;
        protected Model Model => App.Model;
        protected View View => App.View;

        public bool AutoPass = false;
        public bool AutoSwitchTurn = false;

        public void Awake()
        {
            App = App.Get();
        }

        public async Task<int> PlayCard(Card card)
        {
            await Model.MakeMove(new PlayCard(card));
            
            if (AutoSwitchTurn)
            {
                await SwitchTurn();
            }
            else if (PlayedCard)
            {
                View.MakeMoveButton.SwitchTurnWhenPressed();
            }
            
            return 0;
        }

        public async void Pass()
        {
            int choice = !AutoPass ? await View.PromptChoice(
                "Ending turn without playing a card will result in a pass, are you sure?",
                "Yes",
                "No") : 0;

            if (choice == 0)
            {
                await Model.MakeMove(new Pass(AutoSwitchTurn));
                if (!AutoSwitchTurn)
                {
                    View.YourControlledCards.RemovePlayCardSlot();
                    View.MakeMoveButton.SwitchTurnWhenPressed();
                }
            }
        }

        public Task<int> DiscardCards(List<Card> cards) => Model.MakeMove(new DiscardCardFromHand(cards));

        public Task<int> SwitchTurn()
            => Model.MakeMove(new SwitchTurn());

        public Task<int> AddCardsFromDiscardPileToHand(List<Card> cards)
            => Model.MakeMove(new PutDiscardedCardsIntoHand(cards));

        public bool PlayedCard => Model.MoveMakerPlayedCard();

        public Task<int> PutPlayedCardsIntoHand(List<Card> cards)
            => Model.MakeMove(new PutPlayedCardsIntoHand(cards));
    }
}
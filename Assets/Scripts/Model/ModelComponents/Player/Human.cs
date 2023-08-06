using System.Collections.Generic;
using System.Threading.Tasks;

namespace InheritanceFiveCrowns
{
    public class Human : Player
    {
        public Human(App app, string name = "Player") : base(app, name) {}

        #region Control

        protected internal override Task<int> OptionallyChooseCardsToDiscard(int max)
            => View.PromptToOptionallyDiscardCards(max);

        protected internal override Task<int> ChooseCardsToDiscard(int count = 1)
        {
            int maxCount = Hand.Count;
            return View.PromptToDiscardCards(count > maxCount ? maxCount : count);
        }

        protected internal override Task<int> ChooseDiscardedCardsToPutIntoHand(int count = 1)
            => View.PromptUserToPutDiscardedCardsIntoHand(count);

        protected internal override Task<int> ChoosePlayedCardsToPutIntoHand(int count = 1)
            => View.PromptUserToPutPlayedCardsIntoHand(count);

        protected internal override Task<int> OnSwitchTurn()
            => View.OnSwitchTurn(this, Model.PlayerTracker.GetNextPlayer());

        protected internal override async Task<int> OnStartTurn()
        {
            await base.OnStartTurn();
            await View.OnStartTurnCompleted(this);
            return 0;
        }

        protected internal override void DiscardCardsFromHand(List<Card> cards)
        {
            base.DiscardCardsFromHand(cards);
            View.UpdateHand(this);
        }

        protected internal override void RemoveCardsFromHand(List<Card> cards)
        {
            base.RemoveCardsFromHand(cards);
            View.UpdateHand(this);
        }

        protected internal override void AddCardsToHand(List<Card> cards)
        {
            base.AddCardsToHand(cards);
            View.UpdateHand(this);
        }

        protected internal override List<Card> DrawCards(int count = 1)
        {
            List<Card> cards = base.DrawCards(count);
            View.UpdateHand(this);
            return cards;
        }

        protected internal override async Task<int> PlayCard(Card card, bool autoPlay = false)
        {
            if (LastCardLevelPlayed > 0 && card.BaseLevel + LevelModifier > LastCardLevelPlayed)
                return await View.PromptMessage("The played card may not exceed level " + LastCardLevelPlayed);

            if (CardLevelIsBanned(card))
                return await View.PromptMessage("The level of the played card is banned");

            await base.PlayCard(card, autoPlay);
            if (!PlayedCard) return 0;
            
            View.UpdateHand(this);
            View.UpdateYourControlledCards(this);
            return 0;
        }

        #endregion
    }
}
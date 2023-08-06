using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DragAndDrop;
using UnityEngine;
using static InheritanceFiveCrowns.Utility;

namespace InheritanceFiveCrowns
{
    public class DiscardPileView : ViewObject
    {
        protected App App;
        protected View View => App.View;
        protected Controller Controller => App.Controller;
        protected MakeMoveButton MakeMoveButton => View.PlayerChoices.MakeMoveButton;
        protected PlayerHand PlayerHand => View.PlayerHand;

        public CanvasGroup CanvasGroup { protected set; get; }
        public HorizontalScroll HorizontalScroll { protected set; get; }
        public PendingCards PendingCards { protected set; get; }
        public CardContainer CardContainer { protected set; get; }

        protected TaskCompletionSource<int> Tcs;

        public void Awake()
        {
            App = App.Get();
            CanvasGroup = GetComponent<CanvasGroup>();
            HorizontalScroll = GetComponent<HorizontalScroll>();
            PendingCards = GetComponent<PendingCards>();
            CardContainer = GetComponentInChildren<CardContainer>(true);
            CardContainer.CanDragCriteria = CardContainer.CanDragFalse;
            CardContainer.CanDropCriteria = CardContainer.CanDropFalse;
        }

        public void UpdateView(ModelList<Card> discardPile)
        {
            PendingCards.RemoveSlots();
            CardContainer.Clear();
            CardContainer.AddSlotsWithNewCards(discardPile);
            HorizontalScroll.ResizeWidth();
        }

        public void AddPendingCards(int count)
        {
            PendingCards.CreateSlots(count);
            HorizontalScroll.ResizeWidth();

            CardContainer.CanDragCriteria = PendingCards.IsPendingCard;
            CardContainer.CanDropCriteria = PendingCards.IsPendingCardSlot;
        }

        public void RemovePendingCards()
        {
            PendingCards.RemoveSlots();
            HorizontalScroll.ResizeWidth();

            CardContainer.CanDragCriteria = CardContainer.CanDragFalse;
            CardContainer.CanDropCriteria = CardContainer.CanDropFalse;
        }

        public async Task<int> PutDiscardedCardsIntoHand(int count)
        {
            Tcs = new TaskCompletionSource<int>();
            PlayerHand.AddPendingCards(count);
            CardContainer.CanDragCriteria = CardContainer.CanDragTrue;
            CardContainer.CanDropCriteria = CardContainer.CanDropTrue;
            MakeMoveButton.ContinueWhenPressed(ConfirmPutCardsIntoHand);
            await View.PromptMessage("Put " + count + " cards from discard pile into your hand");
            return await Tcs.Task;
        }

        public async void ConfirmPutCardsIntoHand()
        {
            CardContainer.CanDragCriteria = CardContainer.CanDragFalse;
            CardContainer.CanDropCriteria = CardContainer.CanDropFalse;
            await Controller.AddCardsFromDiscardPileToHand(PlayerHand.PendingCards.GetPendingCards());
            PlayerHand.RemovePendingCards();
            Tcs.SetResult(0);
        }

        public Action<bool> SetActive => gameObject.SetActive;
    }

}
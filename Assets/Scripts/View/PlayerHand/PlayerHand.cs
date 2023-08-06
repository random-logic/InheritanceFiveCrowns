using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DragAndDrop;
using UnityEngine;
using static InheritanceFiveCrowns.Utility;

namespace InheritanceFiveCrowns
{
    public class PlayerHand : ViewObject
    {
        protected App App;
        protected View View => App.View;
        protected Controller Controller => App.Controller;

        protected MakeMoveButton MakeMoveButton => View.PlayerChoices.MakeMoveButton;
        protected DiscardPileView DiscardPileView => View.DiscardPileView;


        public HorizontalScroll HorizontalScroll { protected set; get; }
        public PendingCards PendingCards { protected set; get; }
        public CardContainer CardContainer { protected set; get; }

        protected TaskCompletionSource<int> Tcs;

        public void Awake()
        {
            App = App.Get();
            HorizontalScroll = GetComponent<HorizontalScroll>();
            PendingCards = GetComponent<PendingCards>();
            CardContainer = GetComponentInChildren<CardContainer>();
        }

        public void Start()
        {
            SetWidthRelativeTo(View);

            StartCoroutine(WaitForFrames(HorizontalScroll.ResizeWidth));
        }

        public void UpdateView(Player player)
        {
            CardContainer.Clear();
            CardContainer.AddSlotsWithNewCards(player.Hand);
            HorizontalScroll.ResizeWidth();
        }

        public void AddPendingCards(int count = 1)
        {
            PendingCards.CreateSlots(count);
            HorizontalScroll.ResizeWidth();
            CardContainer.CanDragCriteria = PendingCards.IsPendingCard;
            CardContainer.CanDropCriteria = PendingCards.IsPendingCardSlot;
        }

        public void RemovePendingCards()
        {
            CardContainer.CanDragCriteria = CardContainer.CanDragTrue;
            CardContainer.CanDropCriteria = CardContainer.CanDropTrue;
            PendingCards.RemoveSlots();
        }

        public async Task<int> OptionallyDiscardCardsFromHand(int max)
        {
            Tcs = new TaskCompletionSource<int>();
            DiscardPileView.AddPendingCards(max);
            MakeMoveButton.ContinueWhenPressed(ConfirmOptionalDiscardCards);
            await View.PromptMessage("Discard up to " + max + " cards from your hand");
            return await Tcs.Task;
        }

        public async void ConfirmOptionalDiscardCards()
        {
            await Controller.DiscardCards(DiscardPileView.PendingCards.GetPendingCards());
            DiscardPileView.RemovePendingCards();
            Tcs.SetResult(0);
        }

        public async Task<int> DiscardCardsFromHand(int count)
        {
            Tcs = new TaskCompletionSource<int>();
            DiscardPileView.AddPendingCards(count);
            MakeMoveButton.ContinueWhenPressed(ConfirmDiscardCards);
            await View.PromptMessage("Discard " + count + " cards from your hand");
            return await Tcs.Task;
        }

        public async void ConfirmDiscardCards()
        {
            if (HasNull(DiscardPileView.PendingCards.Slots, slot => slot?.Item.Obj))
            {
                await View.PromptMessage("You have to finish discarding before continuing");
                return;
            }

            await Controller.DiscardCards(DiscardPileView.PendingCards.GetPendingCards());
            DiscardPileView.RemovePendingCards();
            Tcs.SetResult(0);
        }

        public Action<bool> SetActive => gameObject.SetActive;
    }
}
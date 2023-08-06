using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DragAndDrop;
using UnityEngine;
using static InheritanceFiveCrowns.Utility;

namespace InheritanceFiveCrowns {
    public class YourControlledCards : ViewObject
    {
        protected App App;
        protected View View => App.View;
        protected Controller Controller => App.Controller;
        protected PlayerChoices PlayerChoices => View.PlayerChoices;
        protected MakeMoveButton MakeMoveButton => View.PlayerChoices.MakeMoveButton;

        public CanvasGroup CanvasGroup { protected set; get; }
        public HorizontalScroll HorizontalScroll { protected set; get; }
        public PendingCards PendingCards { protected set; get; }
        public CardContainer CardContainer { protected set; get; }

        public Slot PlayCardSlot => (PendingCards.Slots?.Count > 0) ? PendingCards.Slots[0] : null;

        public bool AutoPlayCard { protected set; get; }

        protected TaskCompletionSource<int> Tcs;

        public void Awake()
        {
            App = App.Get();
            CanvasGroup = GetComponent<CanvasGroup>();
            HorizontalScroll = GetComponent<HorizontalScroll>();
            PendingCards = GetComponent<PendingCards>();
            CardContainer = GetComponentInChildren<CardContainer>(true);
            SetAutoPlayCard(false);
        }

        public Action<bool> SetActive => gameObject.SetActive;

        public void SetAutoPlayCard(bool autoPlayCard)
        {
            AutoPlayCard = autoPlayCard;
            if (AutoPlayCard) SetAutoPlayCard();
            else SetManualPlayCard();
        }

        public void UpdateView(Player player)
        {
            PendingCards.RemoveSlots();
            CardContainer.Clear();
            CardContainer.AddSlotsWithNewCards(player.Controlled);
            HorizontalScroll.ResizeWidth();
        }

        public void AddPlayCardSlot()
        {
            PendingCards.CreateSlots();
            HorizontalScroll.ResizeWidth();
        }

        protected void SetAutoPlayCard()
        {
            CardContainer.CanDragCriteria = CanDragIfAutoPlayCard;
            CardContainer.CanDropCriteria = CanDropIfAutoPlayCard;
        }

        protected void SetManualPlayCard()
        {
            CardContainer.CanDragCriteria = CanDragIfManualPlayCard;
            CardContainer.CanDropCriteria = CanDropIfManualPlayCard;
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

            SetAutoPlayCard(AutoPlayCard);
        }

        public bool CanDragIfAutoPlayCard(Draggable draggable) => false;

        public bool CanDragIfManualPlayCard(Draggable draggable) => PlayCardSlot != null && draggable == PlayCardSlot.Item;

        public bool CanDropIfAutoPlayCard(DraggableCard draggable, Slot slot)
        {
            if (slot != PlayCardSlot) return false;

            DraggableCard to = slot.Item as DraggableCard;

            if (to.Obj != null) return false;

            to.OnUpdateObject = () => OnAutoPlayCard(to);

            return true;
        }

        public bool CanDropIfManualPlayCard(DraggableCard draggable, Slot slot)
        {
            if (slot != PlayCardSlot) return false;

            DraggableCard to = slot.Item as DraggableCard;

            if (to != null)
                to.OnUpdateObject = () => OnManualPlayCard(to);

            return true;
        }

        public async void OnAutoPlayCard(DraggableCard draggable)
        {
            draggable.OnUpdateObject = null;

            Card card = (draggable.Obj as CardView)?.Card;
            await App.Controller.PlayCard(card);
            if (!Controller.AutoSwitchTurn && Controller.PlayedCard) 
                MakeMoveButton.SwitchTurnWhenPressed();
        }

        public void OnManualPlayCard(DraggableCard draggable)
        {
            draggable.OnUpdateObject = null;

            if (PlayCardSlot != null && PlayCardSlot.Item.Obj != null)
                MakeMoveButton.PlayWhenPressed();
            else
                MakeMoveButton.PassWhenPressed();
        }

        public void RemovePlayCardSlot()
        {
            CardContainer.DeleteSlot(PlayCardSlot);
            RemovePendingCards();
        }

        public async Task<int> PutPlayedCardsIntoHand(int count = 1)
        {
            Tcs = new TaskCompletionSource<int>();
            View.PlayerHand.AddPendingCards(count);
            CardContainer.CanDragCriteria = CardContainer.CanDragTrue;
            CardContainer.CanDropCriteria = CardContainer.CanDropTrue;
            MakeMoveButton.ContinueWhenPressed(ConfirmPlayedCardsIntoHand);
            await View.PromptMessage("Put " + count + " cards from your controlled cards into your hand");
            return await Tcs.Task;
        }

        public void ConfirmPlayedCardsIntoHand()
        {
            Controller.PutPlayedCardsIntoHand(View.PlayerHand.PendingCards.GetPendingCards());
            View.PlayerHand.RemovePendingCards();
            SetAutoPlayCard(AutoPlayCard);
            Tcs.SetResult(0);
        }
    }

}
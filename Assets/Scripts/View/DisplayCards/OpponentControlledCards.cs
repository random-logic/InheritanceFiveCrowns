using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InheritanceFiveCrowns.Utility;

namespace InheritanceFiveCrowns {
    public class OpponentControlledCards : ViewObject
    {
        protected App App;
        protected View View => App.View;
        protected YourControlledCards YourControlledCards => View.YourControlledCards;

        public CanvasGroup CanvasGroup { protected set; get; }
        public HorizontalScroll HorizontalScroll { protected set; get; }
        public CardContainer CardContainer { protected set; get; }
        public PendingCards PendingCards { protected set; get; }

        public void Awake()
        {
            App = App.Get();
            CanvasGroup = GetComponent<CanvasGroup>();
            HorizontalScroll = GetComponent<HorizontalScroll>();
            PendingCards = GetComponent<PendingCards>();
            CardContainer = GetComponentInChildren<CardContainer>(true);
        }

        public void Start()
        {
            CardContainer.CanDropCriteria = CardContainer.CanDropFalse;
            CardContainer.CanDragCriteria = CardContainer.CanDragFalse;
        }

        public Action<bool> SetActive => gameObject.SetActive;

        public void UpdateView(Player opponent)
        {
            CardContainer.Clear();
            CardContainer.AddSlotsWithNewCards(opponent.Controlled);
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
    }

}
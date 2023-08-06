using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using DragAndDrop;
using static InheritanceFiveCrowns.Utility;

namespace InheritanceFiveCrowns {
    public class CardContainer : ObjectContainerWithPooledSlots<CardView>
    {
        public Transform HeightToMatchForCards;
        public float BorderFactor = 0.95f;

        public Func<DraggableCard, Slot, bool> CanDropCriteria;
        public Func<Draggable, bool> CanDragCriteria;

        public static bool CanDropTrue(DraggableCard draggable, Slot slot) => true;
        public static bool CanDragTrue(Draggable draggable) => true;
        public static bool CanDropFalse(DraggableCard draggable, Slot slot) => false;
        public static bool CanDragFalse(Draggable draggable) => false;

        public override bool CanDrop(Draggable dragged, Slot slot)
        {
            DraggableCard draggableCard = dragged as DraggableCard;
            return draggableCard != null && (CanDropCriteria?.Invoke(draggableCard, slot) ?? true);
        }

        public override bool CanDrag(Draggable dragged)
            => CanDragCriteria?.Invoke(dragged) ?? true;

        public void MatchHeightOfAllCardDimensions() {
            foreach (Slot slot in Slots) 
                MatchHeightOfCardDimensions(slot);
        }

        public void MatchHeightOfCardDimensions(Slot slot) {
            CardDimensions[] allCardDimensions = slot.GetComponentsInChildren<CardDimensions>(true);
            // allCardDimensions[0] = slot.CardDimensions
            // allCardDimensions[1] = slot.Item.CardDimensions
            allCardDimensions[0].MatchHeight(HeightToMatchForCards ? HeightToMatchForCards : transform);
            // Leave room for highlighting border via border factor
            allCardDimensions[1].MatchHeight(HeightToMatchForCards ? HeightToMatchForCards : transform, BorderFactor);
        }

        public override Slot AddSlot(CardView obj = null) {
            Slot slot = base.AddSlot(obj);

            MatchHeightOfCardDimensions(slot);
            
            return slot;
        }

        public List<Slot> AddEmptySlots(int count = 1)
        {
            List<Slot> slots = new List<Slot>(count);
            for (int i = 0; i < count; i++)
                slots.Add(AddSlot());
            return slots;
        }

        public Slot AddSlotWithNewCard(Card card)
        {
            CardView cardView = CardView.CreateNew(card);
            return AddSlot(cardView);
        }

        public List<Slot> AddSlotsWithNewCards(List<Card> cards)
        {
            List<Slot> slots = new List<Slot>(cards.Count);
            foreach (Card card in cards)
                slots.Add(AddSlotWithNewCard(card));
            return slots;
        }

        public List<Slot> AddSlotsWithNewCards(ModelList<Card> cards)
        {
            List<Slot> slots = new List<Slot>(cards.Count);
            foreach (Card card in cards)
                slots.Add(AddSlotWithNewCard(card));
            return slots;
        }

        public List<Card> GetCards()
        {
            List<Card> cards = new List<Card>(GetNumberOfSlots());
            cards.AddRange(Slots.Select(slot => (slot.Item.Obj as CardView)?.Card));

            return cards;
        }

        public int GetNumberOfCards()
            => GetNumberOfNotNull(GetCards());
    }

}
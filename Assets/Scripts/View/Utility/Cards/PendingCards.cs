using System.Collections;
using System.Collections.Generic;
using DragAndDrop;
using UnityEngine;
using static InheritanceFiveCrowns.Utility;

namespace InheritanceFiveCrowns
{
    public class PendingCards : MonoBehaviour
    {
        [HideInInspector] public List<Slot> Slots;

        protected CardContainer CardContainer;

        public void Awake()
        {
            CardContainer = GetComponentInChildren<CardContainer>(true);
        }

        // PendingSlots.Remove does not delete the slot
        // Deleting a slot in the PendingSlots list does not remove it from the list
        // PendingSlots.Add does not add to the actual slots
        public void CreateSlots(int count = 1)
            => Slots = CardContainer.AddEmptySlots(count);

        public void RemoveSlots()
            => Slots = null;

        public List<Card> GetPendingCards()
        {
            if (Slots == null) return null;

            List<Card> cards = new List<Card>(Slots.Count);
            foreach (Slot slot in Slots)
            {
                CardView cardView = slot.Item.Obj as CardView;
                if (cardView != null) cards.Add(cardView.Card); // Only add it if it is not null
            }

            return cards;
        }

        // Helper methods
        public bool IsPendingCard(Draggable draggable)
            => !IsEmpty(Slots) && Slots.Contains(draggable.Slot);

        public bool IsPendingCardSlot(Slot slot)
            => !IsEmpty(Slots) && Slots.Contains(slot);

        public bool IsPendingCardSlot(DraggableCard draggable, Slot slot)
            => IsPendingCardSlot(slot);
    }
}
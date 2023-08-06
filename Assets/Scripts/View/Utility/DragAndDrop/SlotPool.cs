using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using DragAndDrop;
using static UnityEngine.Object;

namespace InheritanceFiveCrowns
{
    public class SlotPool
    {
        public ObjectPool<Slot> Pool { protected set; get; }

        protected Slot SlotPrefab;
        protected Draggable ItemPrefab;

        public SlotPool(Slot slotPrefab, Draggable itemPrefab, int defaultCapacity = 10, int maxSize = 10000)
        {
            SlotPrefab = slotPrefab;
            ItemPrefab = itemPrefab;
            Pool = new ObjectPool<Slot>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy, true, defaultCapacity, maxSize);
        }

        protected Slot CreateFunc()
        {
            GameObject go = Instantiate(SlotPrefab.gameObject);
            Slot slot = go.GetComponent<Slot>();

            GameObject goi = Instantiate(ItemPrefab.gameObject, slot.GetSlot());
            Draggable item = goi.GetComponent<Draggable>();

            // Set up pointers
            slot.Item = item;
            item.Slot = slot;

            // This slot does not have pointer to container yet

            return slot;
        }

        protected void ActionOnGet(Slot slot)
        {
            slot.gameObject.SetActive(true);
        }

        protected void ActionOnRelease(Slot slot)
        {
            slot.Item.Obj = null;
            slot.Item.UpdateObject();
            slot.gameObject.SetActive(false);
        }

        protected void ActionOnDestroy(Slot slot)
        {
            Destroy(slot.gameObject);
        }
    }
}
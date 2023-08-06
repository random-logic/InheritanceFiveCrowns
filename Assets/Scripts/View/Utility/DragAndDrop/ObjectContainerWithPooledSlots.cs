using System.Collections;
using System.Collections.Generic;
using DragAndDrop;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public abstract class ObjectContainerWithPooledSlots<T> : ObjectContainer, IListWithFlexibleSlots<T> where T : UnityEngine.Object
    {
        public int DefaultCapacity = 10, MaxCapacity = 10000;

        protected List<Slot> Slots;
        protected SlotPool SlotPool;

        public virtual void Awake()
        {
            SlotPool = new SlotPool(SlotPrefab, ItemPrefab, DefaultCapacity, MaxCapacity);
            Slots = new List<Slot>();
        }

        public override void Drop(Slot slot, ObjectContainer fromContainer)
        {
            // copy the slot's data back into the list we're working with.
            // leave empty
        }

        public virtual Slot AddSlot(T obj)
        {
            Slot slot = SlotPool.Pool.Get();

            // Add this slot as a child to this container
            slot.transform.SetParent(transform);

            // Set this to the end of the container
            slot.transform.SetAsLastSibling();

            // Add a slot to our Slots reference
            Slots.Add(slot);

            // Set pointers
            slot.Container = this;
            slot.Item.SetObject(obj);
            slot.Index = Slots.Count - 1; // Added at the end of the list

            return slot;
        }

        public virtual void DeleteObject(T obj)
        {
            DeleteObjectAt(Slots.FindIndex(slot => slot.Item.Obj == obj));
        }

        public virtual void DeleteObjectIn(Slot slot)
        {
            Draggable item = slot.Item;
            item.Obj = null;
            item.UpdateObject();
        }

        public virtual void DeleteObjectAt(int index)
            => DeleteObjectIn(Slots[index]);

        public virtual void DeleteSlotWith(T obj)
            => DeleteSlotAt(Slots.FindIndex(slot => slot.Item.Obj == obj));

        public virtual void DeleteSlot(Slot slot)
            => DeleteSlotAt(slot.Index);

        public virtual void DeleteSlotAt(int index)
        {
            SlotPool.Pool.Release(Slots[index]);

            Slots.RemoveAt(index);
            for (int i = index; i < Slots.Count; i++)
                //Decrement index of each slot
                Slots[i].Index--;
        }

        public Slot At(int index)
            => Slots[index];

        public void Clear()
        {
            while (Slots.Count > 0)
                DeleteSlot(Slots[0]);
        }

        public int GetNumberOfSlots()
            => Slots.Count;

        public IEnumerator GetEnumerator()
            => ((IEnumerable)Slots).GetEnumerator();
    }
}
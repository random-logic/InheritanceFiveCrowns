using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragAndDrop;
using static InheritanceFiveCrowns.Utility;
using static InheritanceFiveCrowns.ViewObject;

namespace InheritanceFiveCrowns {
    public abstract class ObjectContainerListWithFlexibleSlots<T> : ObjectContainer, IListWithFlexibleSlots<T> where T : UnityEngine.Object {
        protected List<Slot> Slots;

        public void Start() {
            Slots = new List<Slot>();
        }

        public override void Drop(Slot slot, ObjectContainer fromContainer)
        {
            // copy the slot's data back into the list we're working with.
            // leave empty for now
        }

        protected void RemoveAllChildrenIfNoPremadeSlots() {
            if (PreMadeSlots.Length == 0)
                DestroyAllChildren(transform);
        }

        public virtual void CreateSlots(List<T> list) {
            // hook up the appropriate array. This is a reference, so we're now writing to the player data if we change this
            List<T> objects = list;
            Slots = new List<Slot>(list.Count);

            RemoveAllChildrenIfNoPremadeSlots();

            // create a Slot for each object in the list, or use a premade one
            for (int i = 0; i < objects.Count; i++)
            {
                Slot premade = PreMadeSlots != null && PreMadeSlots.Length > i ? PreMadeSlots[i] : null;
                Slot slot = MakeSlot(objects[i], premade);
                slot.Index = i;
                Slots.Add(slot);
            }
        }

        public virtual void CreateSlots(int numOfSlots) {
            List<T> list = GetNewPopulatedList<T>(numOfSlots, null);
            CreateSlots(list);
        }

        // to be called from events
        public void HighlightSlots(bool on)
        {
            foreach (Slot slot in Slots)
            {
                if (on)
                    slot.OnDraggableEnter();
                else
                    slot.OnDraggableExit();
            }
        }

        public virtual Slot AddSlot(T obj, Slot premade) {
            Slot slot = MakeSlot(obj, premade);
            slot.Index = Slots.Count;
            Slots.Add(slot);
            return slot;
        }

        public virtual Slot AddSlot(T obj = null)
        {
            return AddSlot(obj, null);
        }

        public virtual void DeleteObjectIn(Slot slot) {
            DeleteObjectAt(slot.Index);
        }

        public virtual void DeleteObject(T obj) {
            DeleteObjectAt(Slots.FindIndex(slot => slot.Item.Obj == obj));
        }

        public virtual void DeleteObjectAt(int index)
        {
            Draggable item = Slots[index].Item;
            Destroy(item.Obj);
            item.Obj = null;
        }

        public virtual void DeleteSlotWith(T obj) {
            DeleteSlotAt(Slots.FindIndex(slot => slot.Item.Obj == obj));
        }

        public virtual void DeleteSlot(Slot slot) {
            DeleteSlotAt(slot.Index);
        }

        public virtual void DeleteSlotAt(int index) {
            Destroy(Slots[index].gameObject);
            
            Slots.RemoveAt(index);
            for (int i = index; i < Slots.Count; i++)
                //Decrement index of each slot
                Slots[i].Index--;
        }

        public Slot At(int index)
        {
            return Slots[index];
        }

        public void Clear()
        {
            foreach (Slot slot in Slots)
                DeleteSlot(slot);
        }

        public int GetNumberOfSlots()
        {
            return Slots.Count;
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)Slots).GetEnumerator();
        }
    }
}

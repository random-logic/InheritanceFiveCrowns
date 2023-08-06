using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DragAndDrop
{
    public abstract class ObjectContainerList<T> : ObjectContainer where T : UnityEngine.Object
    {
        // Start is called before the first frame update
        protected List<T> Objects;
        protected Slot[] Slots;

        public override void Drop(Slot slot, ObjectContainer fromContainer)
        {
            // copy the slot's data back into the list we're working with.
            Objects[slot.Index] = slot.Item.Obj as T;
        }

        // Use this for initialization
        protected void CreateSlots(List<T> list)
        {
            // hook up the appropriate array. This is a reference, so we're now writing to the player data if we change this
            Objects = list;
            Slots = new Slot[Objects.Count];

            if (PreMadeSlots.Length == 0)
                for (int i = 0; i < transform.childCount; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }

            // create a Slot for each object in the list, or use a premade one
            for (int i = 0; i < Objects.Count; i++)
            {
                Slot premade = PreMadeSlots != null && PreMadeSlots.Length > i ? PreMadeSlots[i] : null;
                Slots[i] = MakeSlot(Objects[i], premade);
                Slots[i].Index = i;
            }
        }

        // to be called from events
        public void HighlightSlots(bool on)
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                if (on)
                    Slots[i].OnDraggableEnter();
                else
                    Slots[i].OnDraggableExit();
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace DragAndDrop
{
    // base class for a UI front end that creates a slot object and child item for each entry
    // marked abstract because it's too general to do anything by itself yet.
    public abstract class ObjectContainer : MonoBehaviour
    {
        // prefab for slot UI element
        public Slot SlotPrefab;

        // prefab for the item UI front ends
        public Draggable ItemPrefab;

        public Slot[] PreMadeSlots;

        [Tooltip("Invoked when an item is first dragged out of this container")]
        public UnityEvent OnDragBegin;
        [Tooltip("Invoked when an item has been dragged out of this container, and that drag has ended")]
        public UnityEvent OnDragEnd;

        [Tooltip("Invoked when an item has been dragged into the wrong slot, and snaps back to its start position")]
        public UnityEvent OnDragFail;

        [Tooltip("Invoked when an item is dragged over one of our slots, entering our airspace")]
        public UnityEvent OnDragEnter;
        [Tooltip("Invoked when the dragged item no longer lies over our slots")]
        public UnityEvent OnDragExit;

        // in the general class, we can drag and drop anything anywhere
        public virtual bool CanDrag(Draggable dragged) { return true; }
        public virtual bool CanDrop(Draggable dragged, Slot slot) { return true; }
        public abstract void Drop(Slot slot, ObjectContainer fromContainer);
        public virtual void ThrowAway(Draggable dragged) { }
        public virtual void OnDraggableBegin()
        {
            OnDragBegin.Invoke();
        }

        public virtual void OnDraggableEnter()
        {
            OnDragEnter.Invoke();
        }

        public virtual void OnDraggableExit()
        {
            OnDragExit.Invoke();
        }

        public virtual bool IsReadOnly()
        {
            return false;
        }

        // create a Slot (or use the optional supplied one) and populate it with the given object
        protected Slot MakeSlot(UnityEngine.Object obj, Slot preMade = null)
        {
            GameObject go = null;
            Slot slot = preMade;

            if (slot != null)
            {
                // use the existing one and get its GameObject
                go = slot.gameObject;
            }
            else
            {
                // make a child slot
                go = Instantiate(SlotPrefab.gameObject, transform);
                slot = go.GetComponent<Slot>();
            }

            // make an item object inside it as a child
            GameObject goi = Instantiate(ItemPrefab.gameObject, slot.GetSlot());
            Draggable item = goi.GetComponent<Draggable>();
            item.SetObject(obj);

            // set up all required pointers
            slot.Item = item;
            slot.Container = this;
            item.Slot = slot;

            return slot;
        }
    }
}
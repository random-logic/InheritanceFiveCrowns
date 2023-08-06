using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace DragAndDrop
{
    // these never move
    // we create one per slot in a backpack or whatever type of container, and each has a child Draggable that gets turned on or off.
    public class Slot : MonoBehaviour
    {
        [Tooltip("Leave as null for this to be the parent of the Draggable placed inside, or set to a child object if required")]
        public Transform SlotTransform;
        // the container that this Slot belongs to
        [HideInInspector]
        public ObjectContainer Container;
        // used by ObjectContainerArray and ObjectContainerList to identify which element of the collection this Slot corresponds to
        [HideInInspector]
        public int Index;

        Image _image;

        public UnityEvent OnDragEnterCanSlot;
        public UnityEvent OnDragEnterCannotSlot;
        public UnityEvent OnDragExit;
        public UnityEvent OnSlot;

        // handy thing for greying out slots that can't be used yet
        bool _isUnlocked;
        public bool Unlocked
        {
            get { return _isUnlocked; }
            set
            {
                _isUnlocked = value;
                if (_image == null)
                    _image = GetComponent<Image>();
                if (_image)
                    _image.color = _isUnlocked ? Color.white : Color.grey;
            }
        }

        public void OnDraggableEnter()
        {
            if (Container != null && Container.CanDrop(Draggable.Current, this))
                OnDragEnterCanSlot.Invoke();
            else
                OnDragEnterCannotSlot.Invoke();
        }

        public void OnDraggableExit()
        {
            OnDragExit.Invoke();
        }

        // get the parent transform for any draggables placed in here.
        public Transform GetSlot()
        {
            return SlotTransform ? SlotTransform : transform;
        }

        // updates the slot's state when the item inside it changes.
        public virtual void UpdateSlot() { }

        // the child item sittign in this slot
        Draggable _item;

        // public accessor that updates the slot when set
        public Draggable Item
        {
            get { return _item;}
            set { _item = value; UpdateSlot(); }
        }
    
    }
}
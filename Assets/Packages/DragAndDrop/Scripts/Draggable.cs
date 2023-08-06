using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DragAndDrop
{
    public abstract class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        protected bool Dragging;
        public static Draggable Current;
        static Slot _currentSlotOver;

        // default to left dragging
        [Tooltip("0 = left mouse, 1 = right mouse to drag")]
        public int MouseButton = 0;

        [HideInInspector]
        public Slot Slot;
        [HideInInspector]
        public UnityEngine.Object Obj;

        // override this in derived classes, usually casting to to the type they deal with
        public abstract void UpdateObject();

        public void SetObject(UnityEngine.Object o)
        {
            (transform as RectTransform).anchoredPosition = Vector3.zero;
            Obj = o;
            UpdateObject();
        }

        Canvas _canvas;

        public void OnBeginDrag(PointerEventData eventData)
        {
            // start dragging with the right mouse, and only if the container will allow it
            if (Input.GetMouseButton(MouseButton))
                Dragging = Slot.Container.CanDrag(this);

            if (Dragging)
            {
                Current = this;
                _currentSlotOver = null;

                if (Slot.Container != null)
                    Slot.Container.OnDraggableBegin();

                // become a sibling of our slot's parent, so we're no longer part of the container (and don't disrupt the GridLayout)
                Transform p = Slot.transform.parent.parent;
                while (p.GetComponent<Canvas>() == null && p != null)
                    p = p.parent;
                transform.SetParent(p);
                // move this to the very front of the UI, so the dragged element draws over everything
                transform.SetAsLastSibling();

                // lazy initialisation to find the canvas we're a part of   
                if (_canvas == null)
                {
                    Transform t = transform;
                    while (t != null && _canvas == null)
                    {
                        t = t.parent;
                        _canvas = t.GetComponent<Canvas>();
                    }
                }
                // move that canvas forwards, so we're dragged on top of other items, and not behind them
                if (_canvas)
                    _canvas.sortingOrder = 1;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            // move the dragged object with the mouse
            if (Dragging)
                transform.position = eventData.position;

            // highlight squares that can receive this component?
            Slot slot = GetSlotUnderMouse();
            if (slot != _currentSlotOver)
            {
                if (slot)
                    slot.OnDraggableEnter();
                if (_currentSlotOver)
                    _currentSlotOver.OnDraggableExit();
                ObjectContainer oldContainer = _currentSlotOver != null ? _currentSlotOver.Container : null;
                ObjectContainer newContainer = slot != null ? slot.Container : null;
                if (oldContainer != null)
                    oldContainer.OnDraggableExit();
                if (newContainer != null)
                    newContainer.OnDraggableEnter();

                _currentSlotOver = slot;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!Dragging)
                return;

            // raycast to find what we're dropping over
            Slot target = GetSlotUnderMouse();

            ObjectContainer containerTarget = null;
            ObjectContainer containerDrag = Slot.Container;

            bool legal = true;

            // check that this move is OK with both containers - they can both veto it
            if (target)
            {
                containerTarget = target.Container;

                // if there is a container and we can't drop into it for game logic reasons, cancel the drag
                if (containerTarget != null)
                    if (containerTarget.CanDrop(this, target) == false)
                        legal = false;

                // check the other way too, since the drag and drop is a swap
                if (containerDrag != null)
                    if (containerDrag.CanDrop(target.Item, Slot) == false)
                        legal = false;
            }

            // we're Ok to move
            if (legal)
            {
                Slot fromSlot = Slot;
                SwapWith(target,
                    containerDrag != null ? containerDrag.IsReadOnly() : true,
                    containerTarget != null ? containerTarget.IsReadOnly() : true);
                // game logic - let both containers know about the update
                if (containerTarget != null)
                    containerTarget.Drop(target, containerDrag);
                if (containerDrag != null)
                    containerDrag.Drop(fromSlot, containerTarget);
            }
            else
            {
                // allow us to make a sound or anything similar that the rejecting container has specified
                containerTarget.OnDragFail.Invoke();
            }

            // return to our parent slot now
            transform.SetParent(Slot.GetSlot());
            (transform as RectTransform).anchoredPosition3D = Vector3.zero;
            Dragging = false;
            Current = null;

            // call the dragexit functions when we're placing, to unhighlight everything.
            // at the slot level...
            if (_currentSlotOver)
                _currentSlotOver.OnDraggableExit();
            // ...and at the container level
            ObjectContainer oldContainer = _currentSlotOver != null ? _currentSlotOver.Container : null;
            if (oldContainer != null)
                oldContainer.OnDraggableExit();
            _currentSlotOver = null;

            if (_canvas)
                _canvas.sortingOrder = 0;
        }

        // this avoids memory allocation each time we move while dragging
        static List<RaycastResult> _hits = new List<RaycastResult>();

        // finds the firstSlot component currently under the mouse
        private Slot GetSlotUnderMouse()
        {
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = Input.mousePosition;
            EventSystem.current.RaycastAll(pointer, _hits);
            foreach (RaycastResult hit in _hits)
            {
                Slot slot = hit.gameObject.GetComponent<Slot>();
                if (slot != null)
                    return slot;
            }
            return null;
        }

        void SwapWith(Slot slot, bool readOnlySource, bool readOnlyTarget)
        {
            if (slot == null)
            {
                // call a virtual function on the container that can eg spawn the item into the world
                if (Slot.Container != null)
                    Slot.Container.ThrowAway(this);

                // dispose if we're throwing the item away, no slot was under the mouse
                if (!readOnlySource)
                    SetObject(null);
            }
            else
            {
                // swap the two valid slot items around
                Draggable other = slot.Item;
                if (other)
                {
                    UnityEngine.Object o = Obj;
                    if (!readOnlySource)
                    {
                        SetObject(other.Obj);
                        if (other.Obj != null && slot != null)
                            slot.OnSlot.Invoke();
                    }
                    if (!readOnlyTarget)
                    {
                        other.SetObject(o);
                        if (o != null && other.Slot != null)
                            other.Slot.OnSlot.Invoke();
                    }
                }
            }
        }
    }
}
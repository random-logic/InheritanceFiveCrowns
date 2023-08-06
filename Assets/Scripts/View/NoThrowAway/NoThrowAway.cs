using System.Collections;
using System.Collections.Generic;
using DragAndDrop;
using UnityEngine;
using static InheritanceFiveCrowns.ViewObject;

namespace InheritanceFiveCrowns
{
    public class NoThrowAway : ObjectContainer
    {
        public override bool CanDrop(Draggable dragged, Slot slot)
        {
            return false;
        }

        public override void Drop(Slot slot, ObjectContainer fromContainer) {}

        public override void ThrowAway(Draggable dragged)
        {
            Debug.Log(dragged.Slot);
        }

        public void Start()
        {
            View view = App.Get().View;
            Slot slot = MakeSlot(null);
            SetWidthRelativeTo(slot.GetSlot(), view);
            SetHeightRelativeTo(slot.GetSlot(), view);
        }
    }
}
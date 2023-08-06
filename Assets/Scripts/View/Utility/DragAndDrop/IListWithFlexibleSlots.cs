using System.Collections;
using System.Collections.Generic;
using DragAndDrop;

namespace InheritanceFiveCrowns
{
    public interface IListWithFlexibleSlots<T> : IEnumerable where T : UnityEngine.Object
    {
        public Slot AddSlot(T obj);
        public void DeleteObject(T obj);
        public void DeleteObjectIn(Slot slot);
        public void DeleteObjectAt(int index);
        public void DeleteSlotWith(T obj);
        public void DeleteSlot(Slot slot);
        public void DeleteSlotAt(int index);
        public Slot At(int index);

        public void Clear();
        public int GetNumberOfSlots();
    }
}
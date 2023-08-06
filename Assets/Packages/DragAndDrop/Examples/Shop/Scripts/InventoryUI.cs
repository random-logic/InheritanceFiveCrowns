using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DragAndDrop;

namespace DragAndDropShop
{
    public class InventoryUi : ObjectContainerArray
    {
        public Inventory Inventory;

        // Start is called before the first frame update
        void Start()
        {
            CreateSlots(Inventory.Items);
        }

        public override void Drop(Slot slot, ObjectContainer fromContainer)
        {
            Item item = slot.Item.Obj as Item;

            if (item)
            {
                if (Inventory.IsPlayer)
                    Inventory.Gold -= item.Cost;

                // if we're taking it out, refund them!
                InventoryUi fromInventoryUi = fromContainer as InventoryUi;
                if (fromInventoryUi && fromInventoryUi.Inventory.IsPlayer)
                    fromInventoryUi.Inventory.Gold += item.Cost;
            }

            base.Drop(slot, fromContainer);
        }

        public override bool CanDrop(Draggable dragged, Slot slot)
        {
            // can always shuffle around inside ourselves.
            if (dragged.Slot.Container == this)
                return true;

            // if this is the player inventory, make sure we have enough money to drop the item in there.
            if (Inventory.IsPlayer)
            {
                // work out the total cost of this transaction
                int cost = 0;
                Item item = dragged.Obj as Item;
                if (item)
                    cost += item.Cost;

                Item itemOut = slot.Item.Obj as Item;
                if (itemOut)
                    cost -= itemOut.Cost;

                return cost <= Inventory.Gold;
            }

            return true;
        }


    }
}

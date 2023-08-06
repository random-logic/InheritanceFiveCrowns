using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragAndDrop;

// example drag and drop container using custom data layout instead of an array
public class EquipScreen : ObjectContainer
{
    public Player Player;

    public Slot Helmet;
    public Slot Amulet;
    public Slot Ring1;
    public Slot Ring2;
    public Slot Gloves;
    public Slot Boots;
    public Slot Armour;

    [HideInInspector]
    public Slot[] Slots;

    void Start()
    {
        Slots = new Slot[7];

        // base these on existing slots set up in UI editor
        // we grab the return value so you could not specify values in the editor and rely on a layout group instead
        Slots[0] = Helmet = MakeSlot(Player.Helmet, Helmet);
        Slots[1] = Amulet = MakeSlot(Player.Amulet, Amulet);
        Slots[2] = Ring1 = MakeSlot(Player.Ring1, Ring1);
        Slots[3] = Ring2 = MakeSlot(Player.Ring2, Ring2);
        Slots[4] = Armour = MakeSlot(Player.Armour, Armour);
        Slots[5] = Gloves = MakeSlot(Player.Gloves, Gloves);
        Slots[6] = Boots = MakeSlot(Player.Boots, Boots);
    }

    // we can drop anything in the right slot
    public override bool CanDrop(Draggable dragged, Slot slot)
    {
        Charm charm = dragged.Obj as Charm;

        // if we're dropping into empty space?
        if (charm == null || slot == null)
            return true;

        // player must meet level requirements
        if (charm.Level > Player.Level)
            return false;

        // only let the right type of item be dropped in this slot
        return (charm.charmType == Charm.CharmType.Helmet && slot == Helmet)
            || (charm.charmType == Charm.CharmType.Amulet && slot == Amulet)
            || (charm.charmType == Charm.CharmType.Ring && (slot == Ring1 || slot == Ring2))
            || (charm.charmType == Charm.CharmType.Gloves && slot == Gloves)
            || (charm.charmType == Charm.CharmType.Boots && slot == Boots)
            || (charm.charmType == Charm.CharmType.Armour && slot == Armour);
    }

    public override void Drop(Slot slot, ObjectContainer fromContainer)
    {
        // write info back into player's data
        Player.Amulet = Amulet.Item.Obj as Charm;
        Player.Helmet = Helmet.Item.Obj as Charm;
        Player.Gloves = Gloves.Item.Obj as Charm;
        Player.Boots = Boots.Item.Obj as Charm;
        Player.Ring1 = Ring1.Item.Obj as Charm;
        Player.Ring2 = Ring2.Item.Obj as Charm;
        Player.Armour = Armour.Item.Obj as Charm;
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DragAndDrop;

public class CharmsArrayUi : ObjectContainerArray {

    // where the data comes from - this represents the player, and either their backpack or belt
    public Player Player;
    public Player.CharmList CharmList;

    public Charm.CharmType CharmType = Charm.CharmType.All;
    public Text Description;

    void Start()
    {
        CreateSlots(Player.GetCharms(CharmList));
    }

    // to be able to drop, it must be a charm of the appropriate type
    public override bool CanDrop(Draggable dragged, Slot slot)
    {
        CharmUi charm = dragged as CharmUi;

        // must be a charm
        if (charm == null)
            return false;

        Charm ch = charm.Obj as Charm;

        // replacing empty slots in here is OK
        if (ch == null)
            return true;

        // check the type of items in the belt vs what we can hold
        int mask = (int)ch.charmType & (int)CharmType;
        return mask != 0;
    }

    public override void OnDraggableBegin()
    {
        // when we start to drag an object, show its name and requirements in the text field provided
        base.OnDraggableBegin();
        if (Description)
        {
            CharmUi charmUi = Draggable.Current as CharmUi;
            if (charmUi)
            {
                Charm charm = charmUi.Obj as Charm;
                if (charm)
                    Description.text = charm.GetDescription();
            }
        }
    }
}

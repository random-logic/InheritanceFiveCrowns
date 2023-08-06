using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DragAndDrop;

using System;

// specialisation of Draggable that displays charms
public class CharmUi : Draggable, IToolTip
{
    public Image Image;

    public string GetToolTipMessage()
    {
        Charm charm = Obj as Charm;
        return (charm == null) ? "" : charm.GetDescription();
    }

    public override void UpdateObject()
    {
        Charm charm = Obj as Charm;

        if (charm == null && Obj != null)
        {
            Debug.LogWarning("Trying to place something that isn't a Charm into a CharmUI!");
        }

        // set the visible data
        if (charm)
        {
            Image.sprite = charm.Icon;
            Image.color = charm.Color;
        }

        // turn off if it was null
        gameObject.SetActive(charm != null);
    }
}

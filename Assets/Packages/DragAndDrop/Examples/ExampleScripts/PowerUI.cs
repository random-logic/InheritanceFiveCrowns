using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DragAndDrop;

public class PowerUi : Draggable {

    public Image Icon;
    public Text Label;

    public override void UpdateObject()
    {
        Power p = Obj as Power;

        // if we have a power
        if (p != null)
        {
            // set the icon
            if (Icon)
            {
                Icon.sprite = p.Icon;
                Icon.color = p.Color;
            }
            // set the label
            if (Label)
                Label.text = p.name;
        }
        // turn off if there is no Power
        gameObject.SetActive(p != null);
    }
}

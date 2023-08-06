using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DragAndDrop;

public class PowerSetSlot : Slot
{
    public Text PowerName;
    public Text PowerDesc;

    // Update is called once per frame
    public override void UpdateSlot()
    {
        PowerUi powerUi = Item as PowerUi;
        if (powerUi)
        {
            Power power = powerUi.Obj as Power;
            if (PowerName)
                PowerName.text = power ? power.name : "";
            if (PowerDesc)
                PowerDesc.text = power ? power.GetDescription() : "";
        }
    }
}

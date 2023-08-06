using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragAndDrop;

public class PowerSetUi : ObjectContainerArray {

    public PowerSet PowerSet;

	// Use this for initialization
	void Start () {
        CreateSlots(PowerSet.Powers);
	}

    // can't change the contents of this one by dragging/dropping
    public override bool IsReadOnly()
    {
        return true;
    }
}

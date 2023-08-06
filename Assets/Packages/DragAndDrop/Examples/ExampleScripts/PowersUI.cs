using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragAndDrop;

public class PowersUi : ObjectContainerList<Power> {

    public Player Player;

	// Use this for initialization
	void Start () {
        CreateSlots(Player.Powers);
	}
}

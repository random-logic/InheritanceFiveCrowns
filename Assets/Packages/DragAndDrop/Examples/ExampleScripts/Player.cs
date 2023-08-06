using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Level;

    public Charm[] Belt = new Charm[4];
    public Charm[] Backpack = new Charm[20];

    public List<Power> Powers = new List<Power>(4);

    [Header("Equipped Items")]
    public Charm Helmet;
    public Charm Amulet;
    public Charm Ring1;
    public Charm Ring2;
    public Charm Gloves;
    public Charm Boots;
    public Charm Armour;

    // accessor stuff for UI to use
    public enum CharmList
    {
        Belt,
        BackPack
    };

    public Charm[] GetCharms(CharmList list)
    {
        switch (list)
        {
            case CharmList.Belt: return Belt;
            case CharmList.BackPack: return Backpack;
            default: return null;
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Charm", menuName = "Charm")]
public class Charm : ScriptableObject {

    public enum CharmType
    {
        Potion = 1,
        Helmet = 2,
        Armour = 4,
        Ring = 8,
        Amulet = 16,
        Boots = 32,
        Gloves = 64,
        All = 127 // combination of all types
    }

    public CharmType charmType = CharmType.Potion;
    public Color Color;
    public float Level;
    public Sprite Icon;

    public string GetDescription()
    {
        // name in bold, plus type
        string desc = "<b>" + name + "</b> (" + charmType.ToString() + ")";

        // optional level requirement on new line
        if (Level > 0)
            desc += "\nRequires Level " + Level;
        return desc;

    }
}

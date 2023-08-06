using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DragAndDropShop
{
    public class Inventory : MonoBehaviour
    {
        public int Gold = 1000;
        public bool IsPlayer = false;
        public Item[] Items;
    }
}
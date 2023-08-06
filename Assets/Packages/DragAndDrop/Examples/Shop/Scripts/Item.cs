using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DragAndDropShop
{
    [CreateAssetMenu(fileName = "Item", menuName = "Item")]
    public class Item : ScriptableObject
    {
        public int Cost;
        public Sprite Icon;
        public Color Color = Color.white;
    }
}
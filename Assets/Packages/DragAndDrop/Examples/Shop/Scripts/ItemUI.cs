using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DragAndDrop;

namespace DragAndDropShop
{
    public class ItemUi : Draggable
    {
        public Text NameTag;
        public Text Cost;
        public Image Icon;

        public override void UpdateObject()
        {
            Item item = Obj as Item;

            if (item != null)
            {
                if (NameTag)
                    NameTag.text = item.name;
                if (Cost)
                    Cost.text = "$" + item.Cost;
                if (Icon)
                {
                    Icon.sprite = item.Icon;
                    Icon.color = item.Color;
                }
            }

            gameObject.SetActive(item != null);
        }
    }
}
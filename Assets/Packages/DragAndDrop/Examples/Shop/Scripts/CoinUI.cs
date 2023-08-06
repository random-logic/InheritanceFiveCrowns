using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DragAndDrop;

namespace DragAndDropShop
{
    public class CoinUi : MonoBehaviour
    {
        public Inventory Inventory;

        Text _text;
        // Start is called before the first frame update
        void Start()
        {
            _text = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            _text.text = "$" + Inventory.Gold;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using InheritanceFiveCrowns;
using TMPro;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class Message : ViewObject
    {
        protected TextMeshProUGUI Tmp;

        public void Awake()
        {
            Tmp = GetComponent<TextMeshProUGUI>();
        }

        public void Set(string message)
        {
            Tmp.text = message;
        }
    }
}
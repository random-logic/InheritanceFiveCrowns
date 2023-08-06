using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InheritanceFiveCrowns
{
    public class Option : ViewObject
    {
        public TextMeshProUGUI Tmp { protected set; get; }
        public Button Button { protected set; get; }

        public Button.ButtonClickedEvent OnClick => Button.onClick;

        public void Awake()
        {
            Tmp = GetComponentInChildren<TextMeshProUGUI>(true);
            Button = GetComponent<Button>();
        }

        public void Set(string message)
        {
            Tmp.text = message;
        }
    }

}
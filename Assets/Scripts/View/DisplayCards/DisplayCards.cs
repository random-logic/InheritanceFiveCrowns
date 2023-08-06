using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InheritanceFiveCrowns
{
    public class DisplayCards : ViewObject
    {
        public Scrollbar VerticalScrollbar { protected set; get; }
        public DiscardPileView DiscardPileView { protected set; get; }
        public OpponentControlledCards OpponentControlledCards { protected set; get; }
        public YourControlledCards YourControlledCards { protected set; get; }

        public void Awake()
        {
            VerticalScrollbar = GetComponentInChildren<Scrollbar>(true);

            DiscardPileView = GetComponentInChildren<DiscardPileView>(true);
            OpponentControlledCards = GetComponentInChildren<OpponentControlledCards>(true);
            YourControlledCards = GetComponentInChildren<YourControlledCards>(true);
        }

        public Action<bool> SetActive => gameObject.SetActive;
    }

}
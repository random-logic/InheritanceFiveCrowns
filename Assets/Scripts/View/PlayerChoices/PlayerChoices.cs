using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InheritanceFiveCrowns {
    public class PlayerChoices : ViewObject
    {
        protected App App;
        protected View View => App.View;

        public MakeMoveButton MakeMoveButton { protected set; get; }

        public void Awake()
        {
            App = App.Get();
            MakeMoveButton = GetComponentInChildren<MakeMoveButton>(true);
        }

        public void Start()
        {
            SetWidthRelativeTo(View);
        }

        public Action<bool> SetActive => gameObject.SetActive;
    }

}
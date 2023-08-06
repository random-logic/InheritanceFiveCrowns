using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InheritanceFiveCrowns {
    public class TopPanel : ViewObject
    {
        protected App App;
        protected View View => App.View;

        public CrownsView CrownsView { protected set; get; }
        public PassesView PassesView { protected set; get; }

        public void Awake()
        {
            App = App.Get();
            CrownsView = GetComponentInChildren<CrownsView>(true);
            PassesView = GetComponentInChildren<PassesView>(true);
        }

        public void Start()
        {
            SetWidthRelativeTo(View);
        }
    }
}
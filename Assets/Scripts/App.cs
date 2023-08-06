using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InheritanceFiveCrowns {
    public class App : MonoBehaviour
    {
        private static App _app;

        public Model Model { protected set; get; }
        public View View { protected set; get; }
        public Controller Controller { protected set; get; }

        public void Awake()
        {
            Model = GetComponentInChildren<Model>(true);
            View = GetComponentInChildren<View>(true);
            Controller = GetComponentInChildren<Controller>(true);
        }

        // There is one and only one app
        // Only find the App once
        public static App Get()
        {
            if (_app == null) _app = FindObjectOfType<App>(); 
            return _app;
        }
    }
}
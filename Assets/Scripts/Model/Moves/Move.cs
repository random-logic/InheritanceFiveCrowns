using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace InheritanceFiveCrowns {
    public abstract class Move
    {
        public App App { internal set; get; }
        
        public Model Model => App.Model;
        public View View => App.View;
        public Controller Controller => App.Controller;

        protected internal abstract Task<int> Enact();
    }
}
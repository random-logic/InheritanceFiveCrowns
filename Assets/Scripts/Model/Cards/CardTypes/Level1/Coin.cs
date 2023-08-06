using System;
using UnityEngine;

namespace InheritanceFiveCrowns {
    public class Coin : Card {
        public Coin(App app) : base(app) {
            Title = "Coin";
            BaseVp = 1;
            BaseLevel = 1;
            Description = "Worth 1 VP";
        }
    }
}
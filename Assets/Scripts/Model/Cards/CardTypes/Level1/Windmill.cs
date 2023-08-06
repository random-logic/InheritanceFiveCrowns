using System;

namespace InheritanceFiveCrowns {
    public class Windmill : Card {
        public Windmill(App app) : base(app) {
            Title = "Windmill";
            BaseLevel = 1;
            Description = "Draw a card";
            OnEnactOnPlayedPowers.Add(new DrawCards());
        }
    }
}
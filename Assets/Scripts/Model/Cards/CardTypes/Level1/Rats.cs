using System;

namespace InheritanceFiveCrowns {
    public class Rats : Card {
        public Rats(App app) : base(app) {
            Title = "Rats";
            BaseVp = -2;
            BaseLevel = 1;
            Description = "Your opponent discards 3 cards";
            OnEnactOnPlayedPowers.Add(new MoveOnNextPlayerStartTurn(new PromptToDiscardCards(3)));
        }
    }
}
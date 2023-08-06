using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InheritanceFiveCrowns {
    public class PlayerTracker : IModelComponent, IEnumerable
    {
        protected App App;
        protected Model Model => App.Model;

        protected List<Player> Players;
        protected int CurrentMoveMakerIndex;

        public PlayerTracker(App app, List<Player> players)
        {
            App = app;
            Players = players;
            (this as IModelComponent).OnResetGame();
        }

        #region Mutators
        internal void SwitchTurns() {
            CurrentMoveMakerIndex = (CurrentMoveMakerIndex + 1) % Players.Count;
        }

        #endregion

        #region Getters
        public Player GetMoveMaker() => Players[CurrentMoveMakerIndex];

        public Player GetNextPlayer(int count = 1)
            => Players[(CurrentMoveMakerIndex + count) % Players.Count];

        public Player GetNextPlayerOf(Player player, int count = 1)
            => Players[IndexOf(player, 1)];

        public int GetTotalPlayers() => Players.Count;
        public Player At(int index) => Players[index];
        public int IndexOf(Player player, int offset = 0) => (Players.IndexOf(player) + offset) % Players.Count;

        public int GetNextPlayerIndexOf(Player player) => IndexOf(player, 1);

        #endregion

        #region Interfaces
        void IModelComponent.OnResetRound()
        {
            Player lastPasser = Model.PassCounter.LastPasser;
            CurrentMoveMakerIndex = lastPasser != null ? GetNextPlayerIndexOf(lastPasser) : 0;
        }

        void IModelComponent.OnResetGame()
        {
            // Do nothing
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)Players).GetEnumerator();
        }

        #endregion
    }
}
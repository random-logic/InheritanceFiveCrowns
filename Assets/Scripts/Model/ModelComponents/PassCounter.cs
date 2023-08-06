namespace InheritanceFiveCrowns {
    public class PassCounter : IModelComponent {
        protected App App;
        protected Model Model => App.Model;

        public int Passes { protected set; get; }
        public int MaxPasses { internal set; get; }
        public int DefaultMaxPasses { internal set; get; }
        public Player LastPasser { protected set; get;}

        public PassCounter(App app, int defaultMaxPasses = 2)
        {
            App = app;
            DefaultMaxPasses = defaultMaxPasses;
        }

        #region Mutators
        internal void IncrementPass(Player passer)
        {
            LastPasser = passer;
            Passes++;
        }
        #endregion

        #region Getters
        public bool PassQuotaReached() {
            return Passes >= MaxPasses;
        }
        #endregion

        #region IModelComponent
        void IModelComponent.OnResetRound()
        {
            Passes = 0;
            MaxPasses = DefaultMaxPasses;
        }

        void IModelComponent.OnResetGame()
        {
            LastPasser = null;
        }
        #endregion
    }
}
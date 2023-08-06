using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class Model : MonoBehaviour, IModelComponent
    {
        #region Initialization

        /*
        // ?? For the future
        public enum GameType
        {
            PassAndPlay, Online
        }
        public GameType CurrentGameType;*/

        // Fields to communicate with Unity UI
        
        protected App App;
        protected View View => App.View;
        
        protected List<IModelComponent> ModelComponents;

        [SerializeField] internal int NumOfCrownsToWin = 5;
        [SerializeField] internal int NumOfCardsToStart = 3;

        internal List<Move> MovesOnStartRound;
        internal List<Move> MovesOnEndRound;
        internal List<Move> MovesAfterDeterminingRoundWinner;

        public CardDeck CardDeck { protected set; get; }
        public DiscardPile DiscardPile { protected set; get; }
        public PassCounter PassCounter { protected set; get; }
        public PlayerTracker PlayerTracker { protected set; get; }
        public Player DeterminedRoundWinner { protected set; get; }
        public Player DeterminedGameWinner { protected set; get; }

        public void Awake()
        {
            App = App.Get();

            CardDeck = new CardDeck(App);
            DiscardPile = new DiscardPile(App);
            PassCounter = new PassCounter(App);
            PlayerTracker = new PlayerTracker(App, new List<Player>
            {
                new Human(App, "Player 1"),
                new Human(App, "Player 2")
            });

            ModelComponents = new List<IModelComponent>();
            foreach (Player player in PlayerTracker)
                ModelComponents.Add(player);
            ModelComponents.Add(CardDeck);
            ModelComponents.Add(DiscardPile);
            ModelComponents.Add(PassCounter);
            ModelComponents.Add(PlayerTracker);
            ModelComponents.Add(this);
        }

        public void Start()
        {
            (this as IModelComponent).OnResetGame();
            StartCoroutine(Utility.WaitForFrames(ResetGame, 3));
        }

        #endregion

        #region Control

        public async Task<int> MakeMove(Move move)
        {
            move.App = App;
            await move.Enact();
            return 0;
        }

        public async Task<int> MakeMoves(List<Move> moves)
        {
            while (moves.Count > 0)
            {
                await MakeMove(moves[0]);
                moves.RemoveAt(0);
            }

            return 0;
        }

        #endregion

        #region Mutators

        internal Player CalculateRoundWinner()
        {
            // Get the highest Vp
            Player playerWithHighestVp = PlayerTracker.At(0);
            bool dupe = false;

            for (int i = 1; i < PlayerTracker.GetTotalPlayers(); i++)
            {
                Player player = PlayerTracker.At(i);
                int vp = player.GetTotalVp();
                int highestVp = playerWithHighestVp.GetTotalVp();
                if (vp > highestVp)
                {
                    playerWithHighestVp = player;
                    dupe = false;
                }
                else if (vp == highestVp)
                    dupe = true;
            }

            // Only one player can have the highest Vp, otherwise no one has the highest Vp
            return dupe ? null : playerWithHighestVp;
        }

        internal async Task<int> DetermineRoundWinner()
        {
            DeterminedRoundWinner = CalculateRoundWinner();
            await MakeMoves(MovesAfterDeterminingRoundWinner);
            return 0;
        }

        internal void IncrementRoundWinnerCrowns() => DeterminedRoundWinner?.IncrementCrowns();

        internal Player CalculateGameWinner()
        {
            // returns null if there is no winner yet
            foreach (Player player in PlayerTracker)
            {
                if (player.NumberOfCrowns >= NumOfCrownsToWin)
                    return player;
            }

            return null;
        }

        internal void DetermineGameWinner() => DeterminedGameWinner = CalculateGameWinner();

        internal void StartGame()
        {
            for (int i = 0; i < NumOfCardsToStart; i++)
                foreach (Player player in PlayerTracker)
                    player.DrawCards();
            ResetRound();
        }

        internal async void StartRound()
        {
            await MakeMoves(MovesOnStartRound);

            Player moveMaker = PlayerTracker.GetMoveMaker();
            await moveMaker.OnSwitchTurn();
            await moveMaker.OnStartTurn();
        }

        internal async void EndRound()
        {
            await MakeMoves(MovesOnEndRound);

            await DetermineRoundWinner();
            if (ExistsRoundWinner())
                IncrementRoundWinnerCrowns();

            await View.OnDeterminedRoundWinner(ExistsRoundWinner(), DeterminedRoundWinner?.Name);

            DetermineGameWinner();
            if (ExistsGameWinner()) EndGame();
            else ResetRound();
        }

        internal void EndGame()
        {
            // ??
            Debug.LogError("NOT IMPLEMENTED MODEL onEndGame()");
            ResetGame();
        }

        internal void ResetGame()
        {
            foreach (var component in ModelComponents) 
                component.OnResetGame();
            StartGame();
        }

        internal void ResetRound()
        {
            foreach (var component in ModelComponents)
            {
                component.OnResetRound();
            }
            StartRound();
        }

        #endregion

        #region Getters

        public bool ExistsRoundWinner() => DeterminedRoundWinner != null;

        public bool ExistsGameWinner() => DeterminedGameWinner != null;

        public bool MoveMakerPlayedCard() => PlayerTracker.GetMoveMaker().PlayedCard;
        #endregion

        #region IModelComponent
        void IModelComponent.OnResetGame()
        {
            MovesOnStartRound = new List<Move>();
            MovesOnEndRound = new List<Move>();
            MovesAfterDeterminingRoundWinner = new List<Move>();
            DeterminedGameWinner = null;
        }

        void IModelComponent.OnResetRound()
        {
            DeterminedRoundWinner = null;
        }

        #endregion
    }
}
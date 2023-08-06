using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public abstract class Player : IModelComponent
    {
        public static int LastCardLevelPlayed { protected internal set; get; }

        public string Name;
        public int NumberOfCrowns { protected internal set; get; }
        public int MaxCardsInHand { protected internal set; get; }

        public int Vp { protected set; get; }
        public int VpMultiplier { protected set; get; }

        public int NumberOfCardsDiscardedPreviously { protected set; get; }

        protected App App;
        protected Model Model => App.Model;
        protected View View => App.View;

        public int LevelModifier { protected internal set; get; }
        public ModelList<int> BannedLevels { protected internal set; get; }

        public ModelList<Card> Hand { protected internal set; get; }
        public ModelList<Card> Controlled { protected internal set; get; }

        protected internal List<Move> MovesOnEndTurn;
        protected internal List<Move> MovesOnStartTurn;

        public bool PlayedCard { protected set; get; }
        public bool Passed { protected set; get; }

        protected Player(App app, string name = "Player", int maxCardsInHand = 10)
        {
            App = app;
            Name = name;
            MaxCardsInHand = maxCardsInHand;
        }

        #region Control

        protected internal abstract Task<int> OptionallyChooseCardsToDiscard(int max);
        protected internal abstract Task<int> ChooseCardsToDiscard(int count = 1);
        protected internal abstract Task<int> ChooseDiscardedCardsToPutIntoHand(int count = 1);
        protected internal abstract Task<int> ChoosePlayedCardsToPutIntoHand(int count = 1);
        #endregion

        #region Callbacks
        protected internal abstract Task<int> OnSwitchTurn();

        protected internal virtual Task<int> OnStartTurn()
        {
            PlayedCard = false;
            Passed = false;
            return Model.MakeMoves(MovesOnStartTurn);
        }

        protected internal virtual async Task<int> OnEndTurn()
        {
            await Model.MakeMoves(MovesOnEndTurn);

            // Game rule hardcoded behavior, but can also add to Moves instead if needed
            if (Hand.Count > MaxCardsInHand)
                await Model.MakeMove(new PromptToDiscardCards(Hand.Count - MaxCardsInHand)); 
            
            return 0;
        }

        #endregion

        #region Mutators
        protected internal virtual void DiscardCardsFromHand(List<Card> cards)
        {
            NumberOfCardsDiscardedPreviously = cards.Count;

            foreach (Card card in cards)
                Hand.Remove(card);
            Model.DiscardPile.Add(cards);
        }

        protected internal virtual void RemoveCardsFromHand(List<Card> cards)
        {
            foreach (Card card in cards)
                Hand.Remove(card);
        }

        protected internal virtual void AddCardsToHand(List<Card> cards)
        {
            foreach (Card card in cards)
                Hand.Add(card);
        }

        protected internal virtual void RemoveCardsFromPlay(List<Card> cards)
        {
            foreach (Card card in cards)
                Controlled.Remove(card);
        }

        protected internal virtual List<Card> DrawCards(int count = 1)
        {
            List<Card> newCards = new List<Card>(count);

            CardDeck cardDeck = Model.CardDeck;
            DiscardPile discardPile = Model.DiscardPile;
            for (int i = 0; i < count; i++)
            {
                discardPile.MoveAllCardsToDeckIfDeckEmpty();

                Card card = cardDeck.Draw();
                Hand.Add(card);
                newCards.Add(card);
            }

            return newCards;
        }

        protected internal virtual async Task<int> PlayCard(Card card, bool autoSwitchTurn = false)
        {
            PlayedCard = true;
            Hand.Remove(card);
            Controlled.Add(card);
            LastCardLevelPlayed = card.BaseLevel;
            await card.OnPlayed(this);
            return 0;
        }

        protected internal void IncrementCrowns() => NumberOfCrowns++;

        protected internal void Pass()
        {
            Passed = true;
            Model.PassCounter.IncrementPass(this);
            ResetLastCardLevelPlayed();
            DrawCards();
        }

        protected internal static void ResetLastCardLevelPlayed()
            => LastCardLevelPlayed = 0;

        #endregion

        #region IModelComponent
        void IModelComponent.OnResetGame()
        {
            NumberOfCrowns = 0;
            MovesOnEndTurn = new List<Move>();
            MovesOnStartTurn = new List<Move>();
            Hand = new ModelList<Card>();
            Controlled = new ModelList<Card>();
        }

        void IModelComponent.OnResetRound()
        {
            Vp = 0;
            VpMultiplier = 1;
            Model.DiscardPile.Add(Controlled);
            Controlled = new ModelList<Card>();
            BannedLevels = new ModelList<int>();
            LastCardLevelPlayed = 0;
        }
        #endregion

        #region Getters

        public int GetVp()
        {
            int vp = 0;

            foreach (Card card in Controlled)
                vp += card.BaseVp + card.AdditionalFixedVp;

            return vp;
        }

        public int GetTotalVp()
        {
            int vp = 0;
            int vpMultiplier = 1;

            foreach (Card card in Controlled)
            {
                vp += card.GetTotalVp();
                vpMultiplier *= card.VpMultiplierForPlayer;
            }

            return vp * vpMultiplier;
        }

        public bool CardLevelIsBanned(Card card)
            => BannedLevels.Contains(card.BaseLevel + LevelModifier);
        #endregion
    }
}
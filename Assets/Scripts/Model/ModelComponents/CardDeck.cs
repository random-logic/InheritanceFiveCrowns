using System;
using System.Collections;
using System.Collections.Generic;
using static InheritanceFiveCrowns.Utility;

namespace InheritanceFiveCrowns {
    public class CardDeck : IModelComponent, IEnumerable {
        // By default, we will choose random element rather than shuffling and then drawing from top
        protected App App;
        protected Model Model => App.Model;

        public ModelList<Card> Cards { internal set; get; }
        public int NumberOfDecks { internal set; get; }

        public CardDeck(App app)
        {
            Cards = new ModelList<Card>();
            App = app;
        }

        #region Mutators
        internal void CreateNewDeck()
        {
            Cards.Container = new List<Card>(18);

            for (int i = 0; i < 18; i++)
            {
                Cards.Add(new Coin(App));
                Cards.Add(new Windmill(App));
                Cards.Add(new Rats(App));
                Cards.Add(new Exchange(App));
                Cards.Add(new Library(App));
                Cards.Add(new Lighthouse(App));
                Cards.Add(new BlackMarket(App));
                Cards.Add(new Supplies(App));
                Cards.Add(new Tax(App));
            }
        }

        internal void Shuffle() {
            Shuffle<Card>(ref Cards.Container);
        }

        internal void AddCards(List<Card> cardsToAdd) {
            foreach (Card card in cardsToAdd)
                Cards.Add(card);
        }

        internal Card DrawRandom() {
            return RemoveRandomElement(Cards.Container);
        }

        internal Card Draw() {
            // draw from the top
            Card card = Cards.At(0);
            Cards.RemoveAt(0);
            return card;
        }

        internal Card DrawAt(int index)
        {
            // in case we want a cheater in the game lol
            Card card = Cards.At(index);
            Cards.RemoveAt(index);
            return card;
        }

        #endregion

        #region Getters
        public Func<Card, int> IndexOf => Cards.IndexOf;

        public bool IsEmpty()
        {
            return Cards.Count == 0;
        }

        #endregion

        #region Interfaces
        void IModelComponent.OnResetRound() {
            // Do nothing unless rules change
        }

        void IModelComponent.OnResetGame() {
            CreateNewDeck();
            Shuffle();
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)Cards).GetEnumerator();
        }

        #endregion
    }
}
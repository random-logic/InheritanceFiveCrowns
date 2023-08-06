using System;
using System.Collections;
using System.Collections.Generic;

namespace InheritanceFiveCrowns {
    public class DiscardPile : IModelComponent, IEnumerable
    {
        protected App App;
        protected Model Model => App.Model;
        protected View View => App.View;
        
        public ModelList<Card> Cards { protected internal set; get; }

        public DiscardPile(App app) {
            App = app;
        }

        #region Mutators
        internal void Add(List<Card> cards)
        {
            foreach (Card card in cards) Cards.Add(card);
            View.OnUpdateDiscardPile(Cards);
        }

        internal void Add(ModelList<Card> cards) => Add(cards.Container);

        internal void Add(Card card)
        {
            Cards.Add(card);
            View.OnUpdateDiscardPile(Cards);
        }

        internal void Remove(List<Card> cards)
        {
            foreach (Card card in cards) Cards.Add(card);
            View.OnUpdateDiscardPile(Cards);
        }

        internal void Remove(ModelList<Card> cards) => Remove(cards.Container);

        internal void Remove(Card card)
        {
            Cards.Remove(card);
            View.OnUpdateDiscardPile(Cards);
        }

        internal void MoveAllCardsToDeck() {
            CardDeck deck = Model.CardDeck;
            deck.AddCards(Cards.Container);
            deck.Shuffle();

            Cards = new ModelList<Card>();
            View.OnUpdateDiscardPile(Cards);
        }

        internal void MoveAllCardsToDeckIfDeckEmpty()
        {
            if (Model.CardDeck.IsEmpty())
                MoveAllCardsToDeck();
        }
        #endregion

        #region Interfaces
        void IModelComponent.OnResetRound() {
            // Do nothing unless game rules change
        }

        void IModelComponent.OnResetGame() {
            Cards = new ModelList<Card>();
            View.OnUpdateDiscardPile(Cards);
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)Cards).GetEnumerator();
        }
        
        #endregion
    }
}
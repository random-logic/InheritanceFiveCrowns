using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace InheritanceFiveCrowns {
    public abstract class Card
    {
        public int BaseLevel { protected set; get; } // Maybe we can convert Vp to a power too
        public int BaseVp { protected set; get; }
        public int AdditionalFixedVp { protected internal set; get; }
        public ModelList<VpCalculation> AdditionalVariableVp { protected internal set; get; } // Cards that need a function for additional variable vp
        public int VpMultiplierForPlayer { protected set; get; }

        // ?? Move this into specific class since these might have different functionality and generic card class has no idea of what the functionality is
        public ModelList<Card> SubCards { protected internal set; get; } // For cards like banking and forge

        public string Title { protected set; get; }
        public string Description { protected set; get; }

        public Player ControlledPlayer { protected set; get; }
        public CardView View { protected set; get; }

        protected List<Move> OnEnactOnPlayedPowers, OnEnactOnGoingPowers, OnRescindOnPlayedPowers, OnRescindOnGoingPowers;

        protected App App;
        protected Model Model => App.Model;

        protected Card(App app) {
            App = app;
            OnEnactOnPlayedPowers = new List<Move>();
            OnEnactOnGoingPowers = new List<Move>();
            BaseVp = 0;
            AdditionalFixedVp = 0;
            AdditionalVariableVp = new ModelList<VpCalculation>();
            SubCards = new ModelList<Card>();
            VpMultiplierForPlayer = 1;
        }

        #region Mutators
        internal async Task<int> EnactAllPowers()
        {
            await EnactOnPlayedPowers();
            await EnactOnGoingPowers();
            return 0;
        }

        internal async Task<int> EnactOnPlayedPowers()
        {
            foreach (Move power in OnEnactOnPlayedPowers)
                await Model.MakeMove(power);
            return 0;
        }

        internal async Task<int> EnactOnGoingPowers()
        {
            foreach (Move power in OnEnactOnGoingPowers)
                await Model.MakeMove(power);
            return 0;
        }

        internal async Task<int> RescindAllPowers()
        {
            await RescindOnPlayedPowers();
            await RescindOnGoingPowers();
            return 0;
        }

        internal async Task<int> RescindOnPlayedPowers()
        {
            foreach (Move power in OnRescindOnPlayedPowers)
                await Model.MakeMove(power);
            return 0;
        }

        internal async Task<int> RescindOnGoingPowers()
        {
            foreach (Move power in OnRescindOnGoingPowers)
                await Model.MakeMove(power);
            return 0;
        }

        internal virtual void AddSubCard(Card card)
        {
            // ??
            card.ControlledPlayer = ControlledPlayer;
            SubCards.Add(card);
        }

        internal virtual void AddSubCards(List<Card> cards)
        {
            foreach (Card card in cards)
                AddSubCard(card);
        }

        internal virtual void AddSubCards(ModelList<Card> cards)
        {
            foreach (Card card in cards)
                AddSubCard(card);
        }

        internal virtual async Task<int> OnDiscarded()
        {
            await RescindAllPowers();
            AdditionalFixedVp = 0;
            AdditionalVariableVp = new ModelList<VpCalculation>();
            ControlledPlayer = null;
            return 0;
        }

        internal virtual async Task<int> OnMovedToPlayerHand(Player player)
        {
            await RescindAllPowers();
            AdditionalFixedVp = 0;
            AdditionalVariableVp = new ModelList<VpCalculation>();
            ControlledPlayer = null;
            return 0;
        }

        internal virtual async Task<int> OnStolen(Player stealer)
        {
            await RescindOnGoingPowers();
            ControlledPlayer = stealer;
            await EnactOnGoingPowers();
            return 0;
        }

        internal virtual async Task<int> OnPlayed(Player player) {
            ControlledPlayer = player;
            await EnactAllPowers();
            return 0;
        }
        #endregion

        #region Getters

        public int GetAdditionalVariableVp()
        {
            int vp = 0;

            foreach (VpCalculation vpCalculation in AdditionalVariableVp)
                vp += vpCalculation.GetVp();

            return vp;
        }

        public int GetTotalVp()
            => BaseVp + AdditionalFixedVp + GetAdditionalVariableVp();

        #endregion
    }
}
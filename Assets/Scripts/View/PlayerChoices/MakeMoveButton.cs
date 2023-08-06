using System;
using InheritanceFiveCrowns;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace InheritanceFiveCrowns
{
    public class MakeMoveButton : ViewObject
    {
        protected App App;
        protected View View => App.View;
        protected Controller Controller => App.Controller;
        protected YourControlledCards YourControlledCards => View.YourControlledCards;
        protected Card CardToPlay => (YourControlledCards.PlayCardSlot.Item.Obj as CardView)?.Card;

        public Button Button { protected set; get; }
        public TextMeshProUGUI TextMeshProView { protected set; get; }

        public Action OnButtonPressed { protected set; get; }

        public void Awake()
        {
            App = App.Get();
            Button = GetComponent<Button>();
            TextMeshProView = GetComponentInChildren<TextMeshProUGUI>(true);
        }

        public void Start()
        {
            Button.onClick.AddListener(() => OnButtonPressed());
        }

        public void PassWhenPressed()
        {
            TextMeshProView.text = "Pass";
            OnButtonPressed = PassButtonPressed;
        }

        protected void PassButtonPressed()
        {
            Controller.Pass();
        }

        public void PlayWhenPressed()
        {
            TextMeshProView.text = "Play";
            OnButtonPressed = PlayButtonPressed;
        }

        protected async void PlayButtonPressed()
        {
            await Controller.PlayCard(CardToPlay);
        }

        public void SwitchTurnWhenPressed()
        {
            TextMeshProView.text = "End Turn";
            OnButtonPressed = SwitchTurnButtonPressed;
        }

        private void SwitchTurnButtonPressed()
        {
            Controller.SwitchTurn();
        }

        public void ContinueWhenPressed(Action action)
        {
            TextMeshProView.text = "Continue";
            OnButtonPressed = action;
        }
    }

}
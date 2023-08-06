using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class Choice : ViewObject
    {
        protected TaskCompletionSource<int> Tcs;

        protected App App;
        protected View View => App.View;

        public CanvasGroup CanvasGroup { protected set; get; }
        public Message Message { protected set; get; }
        public Option Option0 { protected set; get; }
        public Option Option1 { protected set; get; }

        public Action<bool> SetActive => gameObject.SetActive;

        public void Awake()
        {
            App = App.Get();
            CanvasGroup = GetComponent<CanvasGroup>();
            Message = GetComponentInChildren<Message>(true);
            
            Option[] options = GetComponentsInChildren<Option>(true);
            Option0 = options[0];
            Option1 = options[1];
        }

        public void Start()
        {
            Option0.OnClick.AddListener(() => OnSelect(0));
            Option1.OnClick.AddListener(() => OnSelect(1));
        }

        public async Task<int> Prompt(string message, string option0, string option1)
        {
            Tcs = new TaskCompletionSource<int>();
            SetActive(true);
            View.DisplayCards.SetActive(false);
            View.PlayerChoices.SetActive(false);
            View.PlayerHand.SetActive(false);
            Message.Set(message);
            Option0.Set(option0);
            Option1.Set(option1);
            return await Tcs.Task;
        }

        public void OnSelect(int choice)
        {
            SetActive(false);
            View.DisplayCards.SetActive(true);
            View.PlayerChoices.SetActive(true);
            View.PlayerHand.SetActive(true);
            Tcs.SetResult(choice);
        }
    }
}
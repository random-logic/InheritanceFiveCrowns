using System;
using System.Threading.Tasks;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class TapToContinue : ViewObject
    {
        protected TaskCompletionSource<int> Tcs;

        protected App App;
        protected View View => App.View;

        public CanvasGroup CanvasGroup { protected set; get; }
        public Message Message { protected set; get; }

        public Action<bool> SetActive => gameObject.SetActive;

        public void Awake()
        {
            App = App.Get();
            CanvasGroup = GetComponent<CanvasGroup>();
            Message = GetComponentInChildren<Message>(true);
        }

        public Task<int> Prompt(string message)
        {
            Tcs = new TaskCompletionSource<int>();
            SetActive(true);
            View.DisplayCards.SetActive(false);
            View.PlayerChoices.SetActive(false);
            View.PlayerHand.SetActive(false);
            Message.Set(message);
            return Tcs.Task;
        }

        public void OnTap()
        {
            SetActive(false);
            View.DisplayCards.SetActive(true);
            View.PlayerChoices.SetActive(true);
            View.PlayerHand.SetActive(true);
            Tcs.SetResult(0);
        }
    }
}

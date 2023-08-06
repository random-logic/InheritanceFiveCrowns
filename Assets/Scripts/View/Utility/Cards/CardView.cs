using System;
using UnityEngine;
using UnityEngine.UI;

namespace InheritanceFiveCrowns
{
    public class CardView : ScriptableObject
    {
        public bool IsVisible = true;
        public Card Card;

        public static CardView CreateNew(Card card)
        { 
            CardView cardView = CreateInstance<CardView>();
            cardView.Card = card;
            return cardView;
        }

        public Sprite GetFront()
        {
            return Resources.Load<Sprite>("Cards/" + Card.Title);
        }

        public Sprite GetBack()
        {
            return Resources.Load<Sprite>("Cards/Back");
        }

        public Sprite GetSprite()
        {
            return IsVisible ? GetFront() : GetBack();
        }
    }

}
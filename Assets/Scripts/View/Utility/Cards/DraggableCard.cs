using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragAndDrop;
using UnityEngine.UI;

namespace InheritanceFiveCrowns
{
    public class DraggableCard : Draggable
    {
        public bool CardIsVisible => (Obj as CardView)?.IsVisible ?? false;

        public Action OnUpdateObject;

        public Image Image;

        public override void UpdateObject() {
            SetSprite();
            ChangeVisibility();
            OnUpdateObject?.Invoke();
        }

        public void ChangeVisibility()
        {
            gameObject.SetActive(Obj != null);
        }

        public void SetCardVisibility(bool isVisible)
        {
            CardView cardView = (Obj as CardView);
            
            if (cardView == null) return;
            
            cardView.IsVisible = isVisible;
            SetSprite();
        }

        public void SetSprite()
        {
            Image.sprite = (Obj as CardView)?.GetSprite();
        }
    }
}
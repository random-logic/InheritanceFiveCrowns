using System.Collections;
using System.Collections.Generic;
using InheritanceFiveCrowns;
using UnityEngine;

namespace InheritanceFiveCrowns
{
    public class ViewToggle : ViewObject
    {
        public CanvasGroup CanvasGroup { protected set; get; }
        public bool IsActive { protected set; get; }

        public void Awake()
        {
            CanvasGroup = GetComponent<CanvasGroup>();
        }

        public void ToggleView() => SetActive(!IsActive);

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
            SetCanvasGroupActive(CanvasGroup, isActive);
        }
    }
}
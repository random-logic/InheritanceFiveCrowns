using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static InheritanceFiveCrowns.Utility;

// To align left, center, or right, change the anchor points of the content transform
namespace InheritanceFiveCrowns {
    public class HorizontalScroll : ViewObject
    {
        protected Transform ContentTransform;
        protected Transform LayoutTransform;
        protected Transform ScrollbarTransform;

        public void Awake()
        {
            LayoutTransform = GetComponentInChildren<HorizontalLayoutGroup>(true).transform;
            ContentTransform = LayoutTransform.parent;
            ScrollbarTransform = GetComponentInChildren<Scrollbar>(true)?.transform;
        }

        public void Start()
        {
            Resize();
        }

        public void Add(GameObject gameObject) {
            gameObject.transform.parent = LayoutTransform;
            ResizeWidth();
        }

        public void Remove(GameObject gameObject) {
            gameObject.transform.parent = null;
            ResizeWidth();
        }

        public void ResizeWidth() {
            float newWidth = GetWidthOfHorizontalLayout();   
            SetWidth(LayoutTransform, newWidth);
            SetWidth(ContentTransform, newWidth);
        }

        public float GetWidthOfHorizontalLayout() {
            float newWidth = 0.0f;

            foreach (Transform child in LayoutTransform) {
                if (child.gameObject.activeSelf) newWidth += GetWidth(child);
            }

            newWidth += GetTotalSpacingBetweenObjects();
            newWidth += GetLeftAndRightPadding();

            return newWidth;
        }

        public float GetLeftAndRightPadding() {
            return GetLayoutGroup().padding.left + GetLayoutGroup().padding.right;
        }

        public void ResizeHeight() {
            float newHeight = GetHeight() - GetHeightOfOptionalScrollbar();
            SetHeight(ContentTransform, newHeight);
            SetHeight(LayoutTransform, newHeight);
        }

        public float GetHeightOfOptionalScrollbar() {
            return ScrollbarTransform ? GetHeight(ScrollbarTransform) : 0;
        }

        public void Resize() {
            ResizeWidth();
            ResizeHeight();
        }

        public float GetTotalSpacingBetweenObjects() {
            int childCount = 0;
            foreach (Transform child in LayoutTransform)
            {
                if (child.gameObject.activeSelf) childCount++;
            }
            return childCount != 0 ? (childCount - 1) * GetLayoutGroup().spacing : 0;
        }

        public HorizontalLayoutGroup GetLayoutGroup() {
            return LayoutTransform.gameObject.GetComponentInChildren<HorizontalLayoutGroup>();
        }
    }
}
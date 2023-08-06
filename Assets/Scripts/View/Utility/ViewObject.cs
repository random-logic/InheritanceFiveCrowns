using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InheritanceFiveCrowns {
    public class ViewObject : MonoBehaviour
    {
        #region GameObjects
        public static void DestroyAllChildren(Transform transform) {
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }

        public void DestroyAllChildren() {
            DestroyAllChildren(transform);
        }

        public static void SetAllChildrenActive(GameObject gameObject, bool enabled) {
            // transform is enumerable and returns all the children transforms
            foreach (Transform childTransform in gameObject.transform) {
                childTransform.gameObject.SetActive(enabled);
            }
        }

        public static void SetAllChildrenActive(ViewObject view, bool enabled) {
            SetAllChildrenActive(view.gameObject, enabled);
        }

        public void SetAllChildrenActive(bool isActive) {
            SetAllChildrenActive(this, isActive);
        }

        public static void SetCanvasGroupActive(CanvasGroup canvasGroup, bool enabled)
        {
            if (enabled)
            {
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
            else
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
        }
        #endregion

        #region RectTransform
        public static void SetXPosition(Transform transform, float newPos) {
            RectTransform rectTransform = transform as RectTransform;
            rectTransform.anchoredPosition = new Vector2(newPos, rectTransform.anchoredPosition.y);
        }

        public void SetXPosition(float newPos) {
            SetXPosition(transform, newPos);
        }

        public static void SetYPosition(Transform transform, float newPos) {
            RectTransform rectTransform = transform as RectTransform;
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newPos);
        }

        public void SetYPosition(float newPos) {
            SetYPosition(transform, newPos);
        }

        public static void SetHeightRelativeTo(Transform from, Transform to, float scale = 1) {
            SetHeight(from, scale * GetHeight(to));
        }

        public void SetHeightRelativeTo(Transform to, float scale = 1) {
            SetHeightRelativeTo(transform, to, scale);
        }

        public static void SetHeightRelativeTo(Transform from, ViewObject viewObject, float scale = 1)
        {
            SetHeightRelativeTo(from, viewObject.transform, scale);
        }

        public void SetHeightRelativeTo(ViewObject viewObject, float scale = 1)
        {
            SetHeightRelativeTo(transform, viewObject, scale);
        }

        public static void SetWidthRelativeTo(Transform from, Transform to, float scale = 1) {
            SetWidth(from, scale * GetWidth(to));
        }

        public void SetWidthRelativeTo(Transform to, float scale = 1) {
            SetWidthRelativeTo(transform, to, scale);
        }

        public static void SetWidthRelativeTo(Transform from, ViewObject viewObject, float scale = 1) {
            SetWidthRelativeTo(from, viewObject.transform, scale);
        }

        public void SetWidthRelativeTo(ViewObject viewObject, float scale = 1) {
            SetWidthRelativeTo(transform, viewObject.transform, scale);
        }

        public float GetWidth() {
            return GetWidth(transform);
        }

        public static float GetWidth(Transform transform) {
            return (transform as RectTransform).sizeDelta.x;
        }

        public float GetHeight() {
            return GetHeight(transform);
        }

        public static float GetHeight(Transform transform) {
            return (transform as RectTransform).sizeDelta.y;
        }

        public void SetSize(float width, float height) {
            SetSize(this.transform, width, height);
        }

        public void SetWidth(float width) {
            SetSize(width, GetHeight());
        }

        public void SetHeight(float height) {
            SetSize(GetWidth(), height);
        }

        public static void SetSize(Transform transform, float width, float height) {
            (transform as RectTransform).sizeDelta = new Vector2(width, height);
        }

        public static void SetWidth(Transform transform, float width) {
            SetSize(transform, width, GetHeight(transform));
        }

        public static void SetHeight(Transform transform, float height) {
            SetSize(transform, GetWidth(transform), height);
        }
        #endregion
    }
}
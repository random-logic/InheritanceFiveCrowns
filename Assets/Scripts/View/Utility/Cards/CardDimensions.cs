using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InheritanceFiveCrowns {
    public class CardDimensions : ViewObject
    {
        public float Width = 2.5f, Height = 3.5f;

        public Transform HeightToMatch;

        public float BorderFactor = 1f;

        public void Start() {
            if (HeightToMatch) MatchHeight();
        }

        public void MatchHeight(Transform heightToMatch, float borderFactor = 1f) {
            HeightToMatch = heightToMatch;
            BorderFactor = borderFactor;
            MatchHeight();
        }

        public void MatchHeight() {
            ScaleHeight(GetHeight(HeightToMatch) * BorderFactor);
        }

        public void ScaleHeight(float newHeight) {
            float newWidth = Width / Height * newHeight;
            SetSize(newWidth, newHeight);
        }
    }
}
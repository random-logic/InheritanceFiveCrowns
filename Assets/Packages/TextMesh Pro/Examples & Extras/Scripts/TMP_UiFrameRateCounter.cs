﻿using UnityEngine;
using System.Collections;


namespace TMPro.Examples
{
    
    public class TmpUiFrameRateCounter : MonoBehaviour
    {
        public float UpdateInterval = 5.0f;
        private float _mLastInterval = 0;
        private int _mFrames = 0;

        public enum FpsCounterAnchorPositions { TopLeft, BottomLeft, TopRight, BottomRight };

        public FpsCounterAnchorPositions AnchorPosition = FpsCounterAnchorPositions.TopRight;

        private string _htmlColorTag;
        private const string FpsLabel = "{0:2}</color> <#8080ff>FPS \n<#FF8000>{1:2} <#8080ff>MS";

        private TextMeshProUGUI _mTextMeshPro;
        private RectTransform _mFrameCounterTransform;

        private FpsCounterAnchorPositions _lastAnchorPosition;

        void Awake()
        {
            if (!enabled)
                return;

            Application.targetFrameRate = 1000;

            GameObject frameCounter = new GameObject("Frame Counter");
            _mFrameCounterTransform = frameCounter.AddComponent<RectTransform>();

            _mFrameCounterTransform.SetParent(this.transform, false);

            _mTextMeshPro = frameCounter.AddComponent<TextMeshProUGUI>();
            _mTextMeshPro.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF");
            _mTextMeshPro.fontSharedMaterial = Resources.Load<Material>("Fonts & Materials/LiberationSans SDF - Overlay");

            _mTextMeshPro.enableWordWrapping = false;
            _mTextMeshPro.fontSize = 36;

            _mTextMeshPro.isOverlay = true;

            Set_FrameCounter_Position(AnchorPosition);
            _lastAnchorPosition = AnchorPosition;
        }


        void Start()
        {
            _mLastInterval = Time.realtimeSinceStartup;
            _mFrames = 0;
        }


        void Update()
        {
            if (AnchorPosition != _lastAnchorPosition)
                Set_FrameCounter_Position(AnchorPosition);

            _lastAnchorPosition = AnchorPosition;

            _mFrames += 1;
            float timeNow = Time.realtimeSinceStartup;

            if (timeNow > _mLastInterval + UpdateInterval)
            {
                // display two fractional digits (f2 format)
                float fps = _mFrames / (timeNow - _mLastInterval);
                float ms = 1000.0f / Mathf.Max(fps, 0.00001f);

                if (fps < 30)
                    _htmlColorTag = "<color=yellow>";
                else if (fps < 10)
                    _htmlColorTag = "<color=red>";
                else
                    _htmlColorTag = "<color=green>";

                _mTextMeshPro.SetText(_htmlColorTag + FpsLabel, fps, ms);

                _mFrames = 0;
                _mLastInterval = timeNow;
            }
        }


        void Set_FrameCounter_Position(FpsCounterAnchorPositions anchorPosition)
        {
            switch (anchorPosition)
            {
                case FpsCounterAnchorPositions.TopLeft:
                    _mTextMeshPro.alignment = TextAlignmentOptions.TopLeft;
                    _mFrameCounterTransform.pivot = new Vector2(0, 1);
                    _mFrameCounterTransform.anchorMin = new Vector2(0.01f, 0.99f);
                    _mFrameCounterTransform.anchorMax = new Vector2(0.01f, 0.99f);
                    _mFrameCounterTransform.anchoredPosition = new Vector2(0, 1);
                    break;
                case FpsCounterAnchorPositions.BottomLeft:
                    _mTextMeshPro.alignment = TextAlignmentOptions.BottomLeft;
                    _mFrameCounterTransform.pivot = new Vector2(0, 0);
                    _mFrameCounterTransform.anchorMin = new Vector2(0.01f, 0.01f);
                    _mFrameCounterTransform.anchorMax = new Vector2(0.01f, 0.01f);
                    _mFrameCounterTransform.anchoredPosition = new Vector2(0, 0);
                    break;
                case FpsCounterAnchorPositions.TopRight:
                    _mTextMeshPro.alignment = TextAlignmentOptions.TopRight;
                    _mFrameCounterTransform.pivot = new Vector2(1, 1);
                    _mFrameCounterTransform.anchorMin = new Vector2(0.99f, 0.99f);
                    _mFrameCounterTransform.anchorMax = new Vector2(0.99f, 0.99f);
                    _mFrameCounterTransform.anchoredPosition = new Vector2(1, 1);
                    break;
                case FpsCounterAnchorPositions.BottomRight:
                    _mTextMeshPro.alignment = TextAlignmentOptions.BottomRight;
                    _mFrameCounterTransform.pivot = new Vector2(1, 0);
                    _mFrameCounterTransform.anchorMin = new Vector2(0.99f, 0.01f);
                    _mFrameCounterTransform.anchorMax = new Vector2(0.99f, 0.01f);
                    _mFrameCounterTransform.anchoredPosition = new Vector2(1, 0);
                    break;
            }
        }
    }
}
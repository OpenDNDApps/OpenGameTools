using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace OGT
{
    public class UIResponsiveRect : UIItemBase
    {
        [Header("Landscape")] 
        [SerializeField] private RectTransformData m_landscapeData;
        
        [Header("Portrait")]
        [SerializeField] private RectTransformData m_portraitData;

        [SerializeField] private List<UIResponsiveRectTransformDataByScreenOrientation> m_rectsByScreenOrientations = new List<UIResponsiveRectTransformDataByScreenOrientation>();
        
        [Header("On Transition Animation")]
        [SerializeField] private float m_transitionDuration = 0.2f;
        [SerializeField] private Ease m_transitionEase = Ease.InExpo;
        [SerializeField] private RotateMode m_transitionRotationMode = RotateMode.Fast;

        protected override void OnInit()
        {
            base.OnInit();
            ApplyRectConfigByOrientation(UIRuntime.GetCurrentOrientation());
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            UIRuntime.OnScreenOrientationChanged += ApplyRectConfigByOrientation;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UIRuntime.OnScreenOrientationChanged -= ApplyRectConfigByOrientation;
        }

        private void ApplyRectConfigByOrientation(ScreenOrientationFlag orientation)
        {
            RectTransformData data = orientation.HasFlag(ScreenOrientationFlag.Landscape) ? m_landscapeData : m_portraitData;
            if (m_transitionDuration.AlmostEquals(0f))
            {
                data.ApplyTo(RectTransform);
            }
            else
            {
                data.TransitionTo(RectTransform, m_transitionDuration, m_transitionEase, m_transitionRotationMode);
            }
        }
        
        [Button("Save Current RectTransform")]
        public void SaveCurrentRectTransform()
        {
            var screenOrientation = UIRuntime.GetCurrentOrientation();
            if (screenOrientation.HasFlag(ScreenOrientationFlag.Landscape))
            {
                m_landscapeData = new RectTransformData(RectTransform);
            }
            if (screenOrientation.HasFlag(ScreenOrientationFlag.Portrait))
            {
                m_portraitData = new RectTransformData(RectTransform);
            }
        }

        [Button("Test saved Landscape")]
        public void ApplySavedLandscape()
        {
            m_landscapeData.ApplyTo(RectTransform);
        }

        [Button("Test saved Portrait")]
        public void ApplySavedPortrait()
        {
            m_portraitData.ApplyTo(RectTransform);
        }
    }

    [Serializable]
    public struct UIResponsiveRectTransformDataByScreenOrientation
    {
        public ScreenOrientationFlag ScreenOrientation;
        public RectTransformData RectTransformData;
    }

    [Flags]
    public enum ScreenOrientationFlag
    {
        Portrait = 1,
        Landscape = 2,
        Any = 0 | Portrait | Landscape
    }
}
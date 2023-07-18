using System;
using DG.Tweening;
using UnityEngine;

namespace OGT
{
    public partial class UIRuntime
    {
        public static event Action<Resolution> OnResolutionChanged;
        public static event Action<ScreenOrientationFlag> OnScreenOrientationChanged;
        
        private static ScreenOrientationFlag m_currentOrientation;
        private static Resolution m_currentResolution;

        public static ScreenOrientationFlag GetCurrentOrientation() => Screen.width > Screen.height ? ScreenOrientationFlag.Landscape : ScreenOrientationFlag.Portrait;

        partial void OnUIRuntimeResponsivenessInitialize()
        {
            m_currentOrientation = GetCurrentOrientation();
            m_currentResolution = new Resolution
            {
                width = Screen.width,
                height = Screen.height,
            };
            OnResolutionChanged += CheckOrientationChanges;
            OnManualUpdate += OnUIRuntimeResponsivenessUpdate;
        }
        
        private void OnUIRuntimeResponsivenessUpdate()
        {
            CheckResolutionChanges();
        }

        private static void CheckResolutionChanges()
        {
            if (Screen.width == m_currentResolution.width && Screen.height == m_currentResolution.height)
                return;

            m_currentResolution.width = Screen.width;
            m_currentResolution.height = Screen.height;

            OnResolutionChanged?.Invoke(m_currentResolution);
        }

        private static void CheckOrientationChanges(Resolution newResolution)
        {
            ScreenOrientationFlag tickOrientation = GetCurrentOrientation();
            if (tickOrientation == m_currentOrientation)
                return;

            m_currentOrientation = tickOrientation;
            OnScreenOrientationChanged?.Invoke(tickOrientation);
        }
    }
    
    [Serializable]
    public struct RectTransformData
    {
        public Vector2 AnchoredPosition;
        public Quaternion LocalRotation;
        public Vector2 AnchorMin;
        public Vector2 AnchorMax;
        public Vector2 Pivot;
        public Vector2 SizeDelta;

        public RectTransformData(RectTransform target)
        {
            Pivot = target.pivot;
            AnchorMin = target.anchorMin;
            AnchorMax = target.anchorMax;
            SizeDelta = target.sizeDelta;
            AnchoredPosition = target.anchoredPosition;
            LocalRotation = target.localRotation;
        }

        public void ApplyTo(RectTransform target)
        {
            target.localRotation = LocalRotation;
            target.pivot = Pivot;
            target.anchorMin = AnchorMin;
            target.anchorMax = AnchorMax;
            target.sizeDelta = SizeDelta;
            target.anchoredPosition = AnchoredPosition;
        }
        
        public void TransitionTo(RectTransform target, float duration = 0f, Ease ease = Ease.InExpo, RotateMode rotateMode = RotateMode.Fast)
        {
            Sequence transition = DOTween.Sequence();
            transition.Append(target.DOAnchorPos(AnchoredPosition, duration))
                .Join(target.DOLocalRotate(LocalRotation.eulerAngles, duration, rotateMode))
                .Join(target.DOAnchorMin(AnchorMin, duration))
                .Join(target.DOAnchorMax(AnchorMax, duration))
                .Join(target.DOSizeDelta(SizeDelta, duration))
                .Join(target.DOPivot(Pivot, duration))
                .SetEase(ease);
        }
    }

    public static class UIResponsivenessExtensions
    {
        public static ScreenOrientationFlag AsOrientationFlag(this ScreenOrientation orientation)
        {
            switch (orientation)
            {
                case ScreenOrientation.Portrait:
                case ScreenOrientation.PortraitUpsideDown:
                    return ScreenOrientationFlag.Portrait;
                
                case ScreenOrientation.LandscapeLeft:
                case ScreenOrientation.LandscapeRight:
                    return ScreenOrientationFlag.Landscape;
                
                default:
                case ScreenOrientation.AutoRotation:
                #pragma warning disable CS0618
                case ScreenOrientation.Unknown:
                #pragma warning restore CS0618
                    return ScreenOrientationFlag.Any;
            }
        }
        
        public static ScreenOrientationFlag GetOrientation(this Resolution resolution)
        {
            if (resolution.width > resolution.height)
                return ScreenOrientationFlag.Landscape;
            
            if (resolution.width < resolution.height)
                return ScreenOrientationFlag.Portrait;
            
            return ScreenOrientationFlag.Any;
        }
    }
}


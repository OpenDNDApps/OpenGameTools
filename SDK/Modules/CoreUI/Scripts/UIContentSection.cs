using System;
using UnityEngine;

namespace OGT
{
    public class UIContentSection : UIItem
    {
        [SerializeField] private UIContentSectionBehaviours m_contentSectionBehaviours = UIContentSectionBehaviours.DisableOnAwake;
        [SerializeField] private NotchBehaviour m_notchBehaviour = NotchBehaviour.Ignore;
        
        public UIContentSectionBehaviours ContentSectionBehaviours => m_contentSectionBehaviours;
        public NotchBehaviour NotchBehaviour => m_notchBehaviour;

        protected override void OnInit()
        {
            base.OnInit();
            if (m_contentSectionBehaviours.HasFlag(UIContentSectionBehaviours.DisableOnAwake))
            {
                m_itemBehaviours.RemoveFlag(UIItemBehaviours.PlayOnShowAnimationOnEnable);
                m_visualRoots.Disable();
            }

            ApplySafeArea();
        }

        private void ApplySafeArea()
        {
            if (m_notchBehaviour.Equals(NotchBehaviour.Ignore))
                return;

            if (!IsTopLevelWindow)
                return;
            
            Rect safeArea = Screen.safeArea;
            
            Vector2 inverseSize = new Vector2(1f, 1f) / Canvas.pixelRect.size; 
            Vector2 min = Vector2.Scale(safeArea.position, inverseSize);
            Vector2 max = Vector2.Scale(safeArea.position + safeArea.size, inverseSize);
            
            if(m_notchBehaviour.HasFlag(NotchBehaviour.Inverted)) max.y += (1 - max.y) * 2;
            if (!m_notchBehaviour.HasFlag(NotchBehaviour.Top)) max.y = 1f;
            if (!m_notchBehaviour.HasFlag(NotchBehaviour.Right)) max.x = 1f;
            if (!m_notchBehaviour.HasFlag(NotchBehaviour.Bottom)) min.x = 0f;
            if (!m_notchBehaviour.HasFlag(NotchBehaviour.Left)) min.y = 0f;

            RectTransform.anchorMin = min;
            RectTransform.anchorMax = max;

            RectTransform.offsetMin = Vector2.zero;
            RectTransform.offsetMax = Vector2.zero;
        }

        private void FillNotchArea()
        {
            int totalCut = Screen.cutouts.Length;

            if (totalCut <= 0)
                return;
            
            Vector2 anchorMax;
            Vector2 anchorMin;

            Rect[] cutouts = Screen.cutouts;
            for (int i = 0; i < totalCut; i++)
            {
                anchorMin = cutouts[i].position;
                anchorMax = cutouts[i].position + cutouts[i].size;
                anchorMin.x /= Screen.width;
                anchorMin.y /= Screen.height;
                anchorMax.x /= Screen.width;
                anchorMax.y /= Screen.height;
                    
                if (i == 0 && m_notchBehaviour.HasFlag(NotchBehaviour.Top))
                {
                    anchorMin.x = RectTransform.anchorMax.x;
                    RectTransform.anchorMax = anchorMin;
                }
                if (i == 1 && m_notchBehaviour.HasFlag(NotchBehaviour.Bottom))
                {
                    anchorMax.x = RectTransform.anchorMin.x;
                    RectTransform.anchorMin = anchorMax;
                }
            }
        }
    }

    [Flags]
    public enum UIContentSectionBehaviours
    {
        None = 0,
        DisableOnAwake = 1 << 1,
    }
    
    [Flags]
    public enum NotchBehaviour
    {
        Ignore, Inverted = 1, Top = 2, Right = 4, Bottom = 8, Left = 16, Vertical = Top | Bottom, Horizontal = Right | Left, All = Vertical | Horizontal
    }
}

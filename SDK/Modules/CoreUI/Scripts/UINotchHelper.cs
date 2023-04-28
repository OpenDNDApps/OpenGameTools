using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OGT
{
    public static class UINotchHelper
    {
        public static bool IsUsingSafeArea(this RectTransform rectTransform)
        {
            if (!rectTransform.anchorMax.y.Equals(1f))
                return true;
            if (!rectTransform.anchorMax.x.Equals(1f))
                return true;
            if (!rectTransform.anchorMin.y.Equals(0f))
                return true;
            if (!rectTransform.anchorMin.x.Equals(0f))
                return true;
            
            return false;
        }

        public static void ApplySafeArea(this UIWindow window, bool fillNotchWithBlack = false)
        {
            if (window.NotchBehaviour.Equals(NotchBehaviour.Ignore))
                return;

            if (!window.IsTopLevelWindow)
                return;
            
            if(fillNotchWithBlack)
                window.RectTransform.FillNotchArea(window.Canvas, window.NotchBehaviour);
            else
                window.RectTransform.ApplySafeArea(window.Canvas, window.NotchBehaviour);
        }

        public static void ApplySafeArea(this RectTransform rectTransform, Canvas canvas, NotchBehaviour notchBehaviour)
        {
            Rect safeArea = Screen.safeArea;
            
            Vector2 inverseSize = new Vector2(1f, 1f) / canvas.pixelRect.size; 
            Vector2 min = Vector2.Scale(safeArea.position, inverseSize);
            Vector2 max = Vector2.Scale(safeArea.position + safeArea.size, inverseSize);
            
            if(notchBehaviour.HasFlag(NotchBehaviour.Inverted))
            {
                max.y += (1 - max.y) * 2;
            }

            if (!notchBehaviour.HasFlag(NotchBehaviour.Top))
            {
                max.y = 1f;
            }
            
            if (!notchBehaviour.HasFlag(NotchBehaviour.Right))
            {
                max.x = 1f;
            }
            
            if (!notchBehaviour.HasFlag(NotchBehaviour.Bottom))
            {
                min.x = 0f;
            }
            
            if (!notchBehaviour.HasFlag(NotchBehaviour.Left))
            {
                min.y = 0f;
            }

            rectTransform.anchorMin = min;
            rectTransform.anchorMax = max;

            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
        }


        private static void FillNotchArea(this RectTransform rectTransform, Canvas canvas, NotchBehaviour notchBehaviour)
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
                    
                if (i == 0 && notchBehaviour.HasFlag(NotchBehaviour.Top))
                {
                    anchorMin.x = rectTransform.anchorMax.x;
                    rectTransform.anchorMax = anchorMin;
                }
                if (i == 1 && notchBehaviour.HasFlag(NotchBehaviour.Bottom))
                {
                    anchorMax.x = rectTransform.anchorMin.x;
                    rectTransform.anchorMin = anchorMax;
                }
            }
        }
    }
    
    [Flags]
    public enum NotchBehaviour
    {
        Ignore, Inverted = 1, Top = 2, Right = 4, Bottom = 8, Left = 16, Vertical = Top | Bottom, Horizontal = Right | Left, All = Vertical | Horizontal
    }
}

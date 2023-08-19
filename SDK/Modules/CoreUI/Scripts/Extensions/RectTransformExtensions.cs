using DG.Tweening;
using UnityEngine;

public static class RectTransformExtensions
{
    public static void FillToParent(this RectTransform rectTransform)
    {
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
    }

    public static void SetVerticalPosition(this RectTransform rectTransform, float bottomToTop = 0.5f, bool changePivot = true)
    {
        rectTransform.anchorMax = new Vector2(rectTransform.anchorMax.x, bottomToTop);
        rectTransform.anchorMin = new Vector2(rectTransform.anchorMin.x, bottomToTop);

        if (changePivot)
        {
            rectTransform.pivot = new Vector2(rectTransform.pivot.x, bottomToTop);
        }

        rectTransform.anchoredPosition = Vector2.zero;
    }

    // ReSharper disable once InconsistentNaming
    public static void DOVerticalPosition(this RectTransform rectTransform, float duration, float bottomToTop = 0.5f, bool changePivot = true)
    {
        rectTransform.DOAnchorMax(new Vector2(rectTransform.anchorMax.x, bottomToTop), duration);
        rectTransform.DOAnchorMin(new Vector2(rectTransform.anchorMin.x, bottomToTop), duration);

        if (changePivot)
        {
            rectTransform.DOPivotY(bottomToTop, duration);
        }

        rectTransform.DOAnchorPos(Vector2.zero, duration);
    }
}
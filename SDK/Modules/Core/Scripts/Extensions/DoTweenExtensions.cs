using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public static class DoTweenExtensions
{
    /// <summary>Tweens a RectTransform's anchoredPosition as Top distance to the given value.
    /// Also stores the RectTransform as the tween's target so it can be used for filtered operations</summary>
    /// <param name="p_endValue">The end value to reach</param><param name="p_duration">The duration of the tween</param>
    /// <param name="p_snapping">If TRUE the tween will smoothly snap all values to integers</param>
    public static TweenerCore<Vector2, Vector2, VectorOptions> DOOffsetPosTop(this RectTransform p_target, Vector2 p_endValue, float p_duration, bool p_snapping = false)
    {
        TweenerCore<Vector2, Vector2, VectorOptions> l_tweener = DOTween.To(() => p_target.offsetMax, x => p_target.offsetMax = x, -p_endValue, p_duration);
        l_tweener.SetOptions(p_snapping).SetTarget(p_target);
        return l_tweener;
    }
    
    /// <summary>Tweens a RectTransform's anchoredPosition as Bottom distance to the given value.
    /// Also stores the RectTransform as the tween's target so it can be used for filtered operations</summary>
    /// <param name="p_endValue">The end value to reach</param><param name="p_duration">The duration of the tween</param>
    /// <param name="p_snapping">If TRUE the tween will smoothly snap all values to integers</param>
    public static TweenerCore<Vector2, Vector2, VectorOptions> DOOffsetPosBottom(this RectTransform p_target, Vector2 p_endValue, float p_duration, bool p_snapping = false)
    {
        TweenerCore<Vector2, Vector2, VectorOptions> l_tweener = DOTween.To(() => p_target.offsetMin, x => p_target.offsetMin = x, p_endValue, p_duration);
        l_tweener.SetOptions(p_snapping).SetTarget(p_target);
        return l_tweener;
    }
    
    /// <summary>Tweens a RectTransform's anchoredPosition as Left distance to the given value.
    /// Also stores the RectTransform as the tween's target so it can be used for filtered operations</summary>
    /// <param name="p_endValue">The end value to reach</param><param name="p_duration">The duration of the tween</param>
    /// <param name="p_snapping">If TRUE the tween will smoothly snap all values to integers</param>
    public static TweenerCore<Vector2, Vector2, VectorOptions> DOOffsetPosLeft(this RectTransform p_target, Vector2 p_endValue, float p_duration, bool p_snapping = false)
    {
        TweenerCore<Vector2, Vector2, VectorOptions> l_tweener = DOTween.To(() => p_target.offsetMin, y => p_target.offsetMin = y, p_endValue, p_duration);
        l_tweener.SetOptions(p_snapping).SetTarget(p_target);
        return l_tweener;
    }
    
    /// <summary>Tweens a RectTransform's anchoredPosition as Right distance to the given value.
    /// Also stores the RectTransform as the tween's target so it can be used for filtered operations</summary>
    /// <param name="p_endValue">The end value to reach</param><param name="p_duration">The duration of the tween</param>
    /// <param name="p_snapping">If TRUE the tween will smoothly snap all values to integers</param>
    public static TweenerCore<Vector2, Vector2, VectorOptions> DOOffsetPosRight(this RectTransform p_target, Vector2 p_endValue, float p_duration, bool p_snapping = false)
    {
        TweenerCore<Vector2, Vector2, VectorOptions> l_tweener = DOTween.To(() => p_target.offsetMax, y => p_target.offsetMax = y, -p_endValue, p_duration);
        l_tweener.SetOptions(p_snapping).SetTarget(p_target);
        return l_tweener;
    }
    
    /// <summary>Tweens a RectTransform's offsetMin to the given value.
    /// Also stores the RectTransform as the tween's target so it can be used for filtered operations</summary>
    /// <param name="p_endValue">The end value to reach</param><param name="p_duration">The duration of the tween</param>
    /// <param name="p_snapping">If TRUE the tween will smoothly snap all values to integers</param>
    public static TweenerCore<Vector2, Vector2, VectorOptions> DOOffsetMin(this RectTransform p_target, Vector2 p_endValue, float p_duration, bool p_snapping = false)
    {
        TweenerCore<Vector2, Vector2, VectorOptions> l_tweener = DOTween.To(() => p_target.offsetMin, x => p_target.offsetMin = x, p_endValue, p_duration);
        l_tweener.SetOptions(p_snapping).SetTarget(p_target);
        return l_tweener;
    }
    
    /// <summary>Tweens a RectTransform's offsetMax to the given value.
    /// Also stores the RectTransform as the tween's target so it can be used for filtered operations</summary>
    /// <param name="p_endValue">The end value to reach</param><param name="p_duration">The duration of the tween</param>
    /// <param name="p_snapping">If TRUE the tween will smoothly snap all values to integers</param>
    public static TweenerCore<Vector2, Vector2, VectorOptions> DOOffsetMax(this RectTransform p_target, Vector2 p_endValue, float p_duration, bool p_snapping = false)
    {
        TweenerCore<Vector2, Vector2, VectorOptions> l_tweener = DOTween.To(() => p_target.offsetMax, x => p_target.offsetMax = x, p_endValue, p_duration);
        l_tweener.SetOptions(p_snapping).SetTarget(p_target);
        return l_tweener;
    }
}

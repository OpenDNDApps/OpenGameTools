using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace OGT
{
    public static class UIVisualRootExtensions
    {
        public static void Init(this List<UIVisualRoot> visualRootPairs, UIItem item)
        {
            foreach (UIVisualRoot visualRoot in visualRootPairs)
            {
                visualRoot.Init();
                visualRoot.SetOwner(item);
            }
        }
        
        public static void AnimatedShow(this List<UIVisualRoot> visualRoots, Action onComplete)
        {
            foreach (UIVisualRoot visualRoot in visualRoots)
            {
                visualRoot.Disable();
                visualRoot.Activate();
                visualRoot.StartAnimation(VisualRootAnimTriggerType.AnimatedShow, onComplete);
            }
        }
        
        public static void AnimatedHide(this List<UIVisualRoot> visualRoots, Action onComplete)
        {
            foreach (UIVisualRoot visualRoot in visualRoots)
            {
                visualRoot.StartAnimation(VisualRootAnimTriggerType.AnimatedHide, onComplete);
            }
        }
        
        public static List<UIVisualRoot> StartAnimation(this List<UIVisualRoot> visualRoots, VisualRootAnimTriggerType trigger, Action onComplete = null)
        {
            float largestDuration = 0f;
            List<UIVisualRoot> toStartAnimation = visualRoots.GetVisualRootsByTriggerType(trigger);
            if (toStartAnimation.Count == 0)
                return new List<UIVisualRoot>();
            
            foreach (UIVisualRoot visualRoot in visualRoots)
            {
                if (!visualRoot.AnimationsInfo.Exists(pair => pair.TriggerType.HasFlag(trigger)))
                    continue;
                largestDuration = Mathf.Max(largestDuration, visualRoot.StartAnimation(trigger));
            }

            GameResources.Runtime.ActionAfterSeconds(largestDuration, onComplete);
            return toStartAnimation;
        }
        
        public static List<UIVisualRoot> GetVisualRootsByTriggerType(this List<UIVisualRoot> visualRoots, VisualRootAnimTriggerType trigger)
        {
            List<UIVisualRoot> toReturn = new List<UIVisualRoot>();
            foreach (UIVisualRoot visualRoot in visualRoots)
            {
                if (!visualRoot.AnimationsInfo.Exists(pair => pair.TriggerType.HasFlag(trigger)))
                    continue;
                
                toReturn.AddUnique(visualRoot);
            }
            return toReturn;
        }
        
        public static void Enable(this List<UIVisualRoot> visualRootPairs)
        {
            foreach (UIVisualRoot visualRoot in visualRootPairs)
            {
                visualRoot.Enable();
            }
        }
        
        public static void Activate(this List<UIVisualRoot> visualRootPairs)
        {
            foreach (UIVisualRoot visualRoot in visualRootPairs)
            {
                visualRoot.Activate();
            }
        }
        
        public static void Disable(this List<UIVisualRoot> visualRootPairs, bool softDisable = false)
        {
            foreach (UIVisualRoot visualRoot in visualRootPairs)
            {
                visualRoot.Disable(softDisable);
            }
        }
        
        public static void Deactivate(this List<UIVisualRoot> visualRootPairs)
        {
            foreach (UIVisualRoot visualRoot in visualRootPairs)
            {
                visualRoot.Deactivate();
            }
        }
        
        public static void DOKill(this List<UIVisualRoot> visualRoots)
        {
            foreach (var visualRoot in visualRoots)
            {
                visualRoot.DOKill();
            }
        }
        
        public static void HandleOnPointerEnter(this List<UIVisualRoot> visualRootPairs, PointerEventData pointerEventData)
        {
            foreach (UIVisualRoot visualRoot in visualRootPairs)
            {
                visualRoot.HandleOnPointerEnter(pointerEventData);
            }
        }
        
        public static void TriggerOnPointerEnterBehaviour(this List<UIVisualRoot> visualRootPairs, PointerEventData pointerEventData)
        {
            visualRootPairs.GetVisualRootsByTriggerType(VisualRootAnimTriggerType.PointerClick).HandleOnPointerEnter(pointerEventData);
        }
        
        public static void HandleOnPointerExit(this List<UIVisualRoot> visualRootPairs, PointerEventData pointerEventData)
        {
            foreach (UIVisualRoot visualRoot in visualRootPairs)
            {
                visualRoot.HandleOnPointerExit(pointerEventData);
            }
        }
        
        public static void TriggerOnPointerExitBehaviour(this List<UIVisualRoot> visualRootPairs, PointerEventData pointerEventData)
        {
            visualRootPairs.GetVisualRootsByTriggerType(VisualRootAnimTriggerType.PointerClick).HandleOnPointerExit(pointerEventData);
        }
        
        public static void HandleOnPointerClick(this List<UIVisualRoot> visualRootPairs, PointerEventData pointerEventData)
        {
            foreach (UIVisualRoot visualRoot in visualRootPairs)
            {
                visualRoot.HandleOnPointerClick(pointerEventData);
            }
        }
        
        public static void TriggerOnPointerClickBehaviour(this List<UIVisualRoot> visualRootPairs, PointerEventData pointerEventData)
        {
            visualRootPairs.GetVisualRootsByTriggerType(VisualRootAnimTriggerType.PointerClick).HandleOnPointerClick(pointerEventData);
        }
    }
}
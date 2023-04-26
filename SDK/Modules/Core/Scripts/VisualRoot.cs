using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace OGT
{
    public class VisualRoot<TContainer> : BaseBehaviour where TContainer : BodyContainer
    {
        [Header("VisualRoot Settings")]
        [SerializeField] protected string m_id = "MainVisualRoot";
        [SerializeField] protected TContainer m_container;
        
        [Header("UIAnimation Settings")] 
        [SerializeField] protected List<WhenAnimationPair> m_animationsInfo = new List<WhenAnimationPair>();
        
        public List<WhenAnimationPair> AnimationsInfo => m_animationsInfo;

        [Serializable]
        public struct WhenAnimationPair
        {
            public VisualRootAnimTriggerType TriggerType;
            public VisualRoot<TContainer> Animation;
        }

        public virtual float StartAnimation(VisualRootAnimTriggerType triggerType, Action onComplete = null)
        {
            return 0f;
        }

        protected override void OnInit()
        {
            m_container = GetComponent<TContainer>();
        }
    }

    public static class VisualRootExtensions
    {
        public static List<VisualRoot<T>> StartAnimation<T>(this List<VisualRoot<T>> visualRoots, VisualRootAnimTriggerType trigger, Action onComplete = null) where T : BodyContainer
        {
            float largestDuration = 0f;
            List<VisualRoot<T>> toStartAnimation = visualRoots.GetVisualRootsByTriggerType(trigger);
            foreach (var visualRoot in visualRoots)
            {
                if (!visualRoot.AnimationsInfo.Exists(pair => pair.TriggerType.HasFlag(trigger)))
                    continue;
        
                largestDuration = Mathf.Max(largestDuration, visualRoot.StartAnimation(trigger));
            }
        
            GameRuntime.Instance.ActionAfterSeconds(largestDuration, onComplete);
            return toStartAnimation;
        }
        
        public static List<VisualRoot<T>> GetVisualRootsByTriggerType<T>(this List<VisualRoot<T>> visualRoots, VisualRootAnimTriggerType trigger) where T : BodyContainer
        {
            List<VisualRoot<T>> toReturn = new List<VisualRoot<T>>();
            foreach (var visualRoot in visualRoots)
            {
                if (!visualRoot.AnimationsInfo.Exists(pair => pair.TriggerType.HasFlag(trigger)))
                    continue;
                
                toReturn.AddUnique(visualRoot);
            }
            return toReturn;
        }
    
        public static void Disable<T>(this List<VisualRoot<T>> visualRootPairs, bool softDisable = false) where T : BodyContainer
        {
            foreach (var visualRoot in visualRootPairs)
            {
                visualRoot.Disable(softDisable);
            }
        }
    
        public static void Enable<T>(this List<VisualRoot<T>> visualRootPairs) where T : BodyContainer
        {
            foreach (var visualRoot in visualRootPairs)
            {
                visualRoot.Enable();
            }
        }
    
        public static void DOKill<T>(this List<VisualRoot<T>> visualRoots) where T : BodyContainer
        {
            foreach (var visualRoot in visualRoots)
            {
                visualRoot.DOKill();
            }
        }
    }

}
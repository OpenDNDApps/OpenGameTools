using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace OGT
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIVisualRoot : UIItemBase
    {
        [Header("UIVisualRoot Settings")]
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private UIVisualRootBehaviours m_visualRootBehaviours = UIVisualRootBehaviours.None;
        
        [Header("UIAnimation Settings")] 
        [SerializeField] private List<WhenAnimationPair> m_animationsInfo = new List<WhenAnimationPair>();
        
        private VisualRootAnimTriggerType m_currentTriggerType = VisualRootAnimTriggerType.None;
        
        public List<WhenAnimationPair> AnimationsInfo => m_animationsInfo;
        public Action<VisualRootAnimTriggerType> OnAnimationTriggered;
        public UIVisualRootBehaviours VisualRootBehaviours => m_visualRootBehaviours;

        protected override void OnInit()
        {
            m_canvasGroup = GetComponent<CanvasGroup>();

            if (!m_visualRootBehaviours.HasFlag(UIVisualRootBehaviours.IgnoreDefaultSelfGeneration))
            {
                if (!m_animationsInfo.Exists(pair => pair.TriggerType.HasFlag(VisualRootAnimTriggerType.AnimatedShow)))
                {
                    m_animationsInfo.Add(new WhenAnimationPair
                    {
                        TriggerType = VisualRootAnimTriggerType.AnimatedShow,
                        Animation = GameResources.Settings.UI.Default.ShowAnimation,
                    });
                }

                if (!m_animationsInfo.Exists(pair => pair.TriggerType.HasFlag(VisualRootAnimTriggerType.AnimatedHide)))
                {
                    m_animationsInfo.Add(new WhenAnimationPair
                    {
                        TriggerType = VisualRootAnimTriggerType.AnimatedHide,
                        Animation = GameResources.Settings.UI.Default.HideAnimation,
                    });
                }
            }
            base.OnInit();
        }
        
        private bool HasAnyAnimationsOfType(VisualRootAnimTriggerType trigger)
        {
            return m_animationsInfo.Any(pair => pair.TriggerType.HasFlag(trigger));
        }

        public virtual float StartAnimation(VisualRootAnimTriggerType triggerType, Action onComplete = null)
        {
            if(triggerType == m_currentTriggerType)
                return 0f;
            m_currentTriggerType = triggerType;
            
            if (!HasAnyAnimationsOfType(triggerType))
                return 0f;
            
            OnAnimationTriggered?.Invoke(triggerType);

            return StartAnimationsOnTriggerMatches(m_animationsInfo, triggerType, onComplete);
        }

        public float StartAnimationsOnTriggerMatches(List<WhenAnimationPair> animPair, VisualRootAnimTriggerType trigger, Action onComplete = null)
        {
            float largestDuration = 0f;
            
            foreach (WhenAnimationPair pair in animPair)
            {
                if (!pair.TriggerType.HasFlag(trigger))
                    continue;

                UIAnimation theAnimation = null;
                if (pair.TriggerType.HasFlag(VisualRootAnimTriggerType.AnimatedShow))
                {
                    theAnimation = theAnimation != null ? theAnimation : pair.Animation != null ? pair.Animation : GameResources.Settings.UI.Default.ShowAnimation;
                    Disable();
                }
                if (pair.TriggerType.HasFlag(VisualRootAnimTriggerType.AnimatedHide))
                {
                    theAnimation = theAnimation != null ? theAnimation : pair.Animation != null ? pair.Animation : GameResources.Settings.UI.Default.HideAnimation;
                }
                theAnimation = theAnimation != null ? theAnimation : pair.Animation != null ? pair.Animation : GameResources.Settings.UI.Default.EmptyAnimation;
            
                UIAnimation animClone = Instantiate(theAnimation);
                if (TryInsertDynamicDelay(pair, out float delay))
                {
                    animClone.InsertDelay(delay);
                }
                
                HandleVisualBehaviour(pair);
                
                Activate();
                animClone.StartAnimation(m_canvasGroup);
                AudioRuntime.Play(pair.OnTriggeredSound);
            
                largestDuration = Mathf.Max(largestDuration, theAnimation.OverallDuration);
            }

            this.ActionAfterSeconds(largestDuration, () =>
            {
                ResetState();
                onComplete?.Invoke();
            });

            return largestDuration;
        }

        private bool TryInsertDynamicDelay(WhenAnimationPair pair, out float delay)
        {
            if (pair.VisualBehaviour.HasFlag(WhenVisualBehaviour.InsertParentPositionBasedDelay))
            {
                delay = transform.parent.GetSiblingIndex() * GameResources.Settings.UI.Default.PositionBasedAnimationDelay;
                return true;
            }
            if(pair.VisualBehaviour.HasFlag(WhenVisualBehaviour.InsertSiblingPositionBasedDelay))
            {
                delay = transform.GetSiblingIndex() * GameResources.Settings.UI.Default.PositionBasedAnimationDelay;
                return true;
            }
            delay = 0f;
            return false;
        }

        private void HandleVisualBehaviour(WhenAnimationPair pair)
        {
            switch (pair.VisualBehaviour)
            {
                default:
                case WhenVisualBehaviour.Nothing:
                    return;
                case WhenVisualBehaviour.SetVisualRootAsFirstSibling:
                    transform.SetAsFirstSibling();
                    break;
                case WhenVisualBehaviour.SetVisualRootAsLastSibling:
                    transform.SetAsLastSibling();
                    break;
                case WhenVisualBehaviour.SetParentAsFirstSibling:
                    transform.parent.SetAsFirstSibling();
                    break;
                case WhenVisualBehaviour.SetParentAsLastSibling:
                    transform.parent.SetAsLastSibling();
                    break;
            }
        }

        public void Enable()
        {
            if (HasAnyAnimationsOfType(VisualRootAnimTriggerType.OnShowOrEnable))
            {
                StartAnimation(VisualRootAnimTriggerType.OnShowOrEnable);
            }
            else
            {
                m_canvasGroup.Enable();
            }
        }

        public override void Activate()
        {
            base.Activate();
            m_canvasGroup.Activate();
        }

        public void Disable(bool p_softDisable = false)
        {
            m_canvasGroup.Disable(p_softDisable);
        }
        
        public override void Deactivate()
        {
            m_canvasGroup.Deactivate();
            base.Deactivate();
        }

        public virtual void HandleOnPointerEnter(PointerEventData _)
        {
            StartAnimation(VisualRootAnimTriggerType.PointerEnter);
        }

        public virtual void HandleOnPointerExit(PointerEventData _)
        {
            StartAnimation(VisualRootAnimTriggerType.PointerExit);
        }

        public virtual void HandleOnPointerClick(PointerEventData _)
        {
            StartAnimation(VisualRootAnimTriggerType.PointerClick);
        }

        public void AnimatedShow()
        {
            Activate();
            StartAnimation(VisualRootAnimTriggerType.AnimatedShow);
        }

        public void AnimatedHide()
        {
            StartAnimation(VisualRootAnimTriggerType.AnimatedHide);
        }

        public void ResetState()
        {
            m_currentTriggerType = VisualRootAnimTriggerType.None;
        }
    }

    [Serializable]
    public struct WhenAnimationPair
    {
        public VisualRootAnimTriggerType TriggerType;
        public UIAnimation Animation;
        public AudioClipDefinition OnTriggeredSound;
        public WhenVisualBehaviour VisualBehaviour;
    }
        
    [Flags]
    public enum UIVisualRootBehaviours
    {
        None = 0,
        IgnoreDefaultSelfGeneration = 1 << 0,
    }

    [Flags]
    public enum WhenVisualBehaviour
    {
        Nothing = 0,
        SetVisualRootAsFirstSibling = 1 << 1,
        SetVisualRootAsLastSibling = 1 << 2,
        SetParentAsFirstSibling = 1 << 3,
        SetParentAsLastSibling = 1 << 4,
        InsertSiblingPositionBasedDelay = 1 << 5,
        InsertParentPositionBasedDelay = 1 << 6,
    }

    [System.Flags]
    public enum VisualRootAnimTriggerType
    {
        None = -1,
        Manual = 0,
        AnimatedShow = 1 << 0,
        AnimatedHide = 1 << 1,
        OnShowOrEnable = 1 << 2,
        //Deactivate = 1 << 3, // use ExitAnimation instead.
        PointerEnter = 1 << 4,
        PointerExit = 1 << 5,
        PointerClick = 1 << 6,
    }
}

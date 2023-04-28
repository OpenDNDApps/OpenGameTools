using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace OGT
{
    [CreateAssetMenu(fileName = "UIAnimation_", menuName = GameResources.kPluginName + "/UI/Animation")]
    public class UIAnimation : ScriptableAnimation<UIAnimationStep>
    {
        public virtual void StartAnimation(CanvasGroup target, Action onComplete = null, Animator targetAnimator = null)
        {
            if (Steps.Count == 0)
            {
                onComplete?.Invoke();
                return;
            }
            
            if (target == default)
            {
                Debug.LogWarning($"[UIAnimation] Warning: target or visualRoot are missing on '{name}', defaulting to OnComplete.");
                onComplete?.Invoke();
                return;
            }
            
            target.DOKill(true);
            Sequence sequence = DOTween.Sequence(target);

            foreach (UIAnimationStep step in Steps)
            {
                switch (step.Type)
                {
                    case UIAnimationStepType.Animation:
                        Animator animator = targetAnimator;
                        if (animator == default)
                            animator = target.GetOrAddComponent<Animator>();
                        
                        animator.runtimeAnimatorController = step.Animation.Animator;
                        animator.enabled = true;
                        sequence.JoinByStepType(step.JoinType, DOVirtual.Float(0f, 1f, step.Animation.Params.Duration, currentValue => {
                            if (animator == default)
                                return;
                            animator.SetFloat(step.Animation.MotionKey, currentValue);
                        }).SetDelay(step.Animation.Params.Delay).SetEase(step.Animation.Params.Ease));
                    break;
                    case UIAnimationStepType.Alpha:
                        sequence.JoinByStepType(step.JoinType, target.DOFade(step.Alpha.TargetValue, step.Alpha.Duration).SetDelay(step.Alpha.Delay).SetEase(step.Alpha.Ease));
                    break;
                    case UIAnimationStepType.Scaling:
                        sequence.JoinByStepType(step.JoinType, target.transform.DOScale(step.Scaling.TargetValue, step.Scaling.Duration).SetDelay(step.Scaling.Delay).SetEase(step.Scaling.Ease));
                    break;
                    case UIAnimationStepType.AnchorMin:
                        sequence.JoinByStepType(step.JoinType, ((RectTransform)target.transform).DOAnchorMin(step.AnchorMin.TargetValue, step.AnchorMin.Duration).SetDelay(step.AnchorMin.Delay).SetEase(step.AnchorMin.Ease));
                    break;
                    case UIAnimationStepType.AnchorMax:
                        sequence.JoinByStepType(step.JoinType, ((RectTransform)target.transform).DOAnchorMax(step.AnchorMin.TargetValue, step.AnchorMin.Duration).SetDelay(step.AnchorMin.Delay).SetEase(step.AnchorMin.Ease));
                    break;
                    case UIAnimationStepType.AnchorPositions:
                        sequence.JoinByStepType(step.JoinType, ((RectTransform)target.transform).DOAnchorPos(step.AnchorPositions.TargetValue, step.AnchorPositions.Duration).SetDelay(step.AnchorPositions.Delay).SetEase(step.AnchorPositions.Ease));
                    break;
                    case UIAnimationStepType.AnchorPositionX:
                        sequence.JoinByStepType(step.JoinType, ((RectTransform)target.transform).DOAnchorPosX(step.AnchorPositionX.TargetValue, step.AnchorPositionX.Duration).SetDelay(step.AnchorPositionX.Delay).SetEase(step.AnchorPositionX.Ease));
                    break;
                    case UIAnimationStepType.AnchorPositionY:
                        sequence.JoinByStepType(step.JoinType, ((RectTransform)target.transform).DOAnchorPosY(step.AnchorPositionY.TargetValue, step.AnchorPositionY.Duration).SetDelay(step.AnchorPositionY.Delay).SetEase(step.AnchorPositionY.Ease));
                    break;
                }
            }
            
            sequence.OnComplete(() => onComplete?.Invoke());
            sequence.Play();
        }

        protected override float GetDurationByStepType(BaseAnimationStep baseStep)
        {
            float stepDuration = 0f;
            var step = (UIAnimationStep)baseStep;
            
            switch (step.Type)
            {
                case UIAnimationStepType.Alpha:
                    stepDuration = step.Alpha.Duration + step.Alpha.Delay;
                break;
                case UIAnimationStepType.Scaling:
                    stepDuration = step.Scaling.Duration + step.Scaling.Delay;
                break;
                case UIAnimationStepType.Animation:
                    stepDuration = step.Animation.Params.Duration + step.Animation.Params.Delay;
                break;
                case UIAnimationStepType.AnchorMin:
                    stepDuration = step.AnchorMin.Duration + step.AnchorMin.Delay;
                break;
                case UIAnimationStepType.AnchorMax:
                    stepDuration = step.AnchorMax.Duration + step.AnchorMax.Delay;
                break;
                case UIAnimationStepType.AnchorPositions:
                    stepDuration = step.AnchorPositions.Duration + step.AnchorPositions.Delay;
                break;
                case UIAnimationStepType.AnchorPositionX:
                    stepDuration = step.AnchorPositionX.Duration + step.AnchorPositionX.Delay;
                break;
                case UIAnimationStepType.AnchorPositionY:
                    stepDuration = step.AnchorPositionY.Duration + step.AnchorPositionY.Delay;
                break;
            }

            return stepDuration;
        }

        public virtual void InsertDelay(float p_delay)
        {
            UIAnimationStep step = default;
            bool found = false;
            foreach (UIAnimationStep iStep in Steps)
            {
                if(iStep.JoinType == ScriptableAnimationJoinType.Join)
                    continue;

                found = true;
                step = iStep;
                break;
            }
        
            if(found == false)
                return;

            switch (step.Type)
            {
                case UIAnimationStepType.Alpha:
                    step.Alpha.Delay += p_delay;
                    break;
                case UIAnimationStepType.Scaling:
                    step.Scaling.Delay += p_delay;
                    break;
                case UIAnimationStepType.Animation:
                    step.Animation.Params.Delay += p_delay;
                    break;
                case UIAnimationStepType.AnchorMin:
                    step.AnchorMin.Delay += p_delay;
                    break;
                case UIAnimationStepType.AnchorMax:
                    step.AnchorMax.Delay += p_delay;
                    break;
                case UIAnimationStepType.AnchorPositions:
                    step.AnchorPositions.Delay += p_delay;
                    break;
                case UIAnimationStepType.AnchorPositionX:
                    step.AnchorPositionX.Delay += p_delay;
                    break;
                case UIAnimationStepType.AnchorPositionY:
                    step.AnchorPositionY.Delay += p_delay;
                    break;
            }
        }
    }

    [Serializable]
    public enum UIAnimationStepType
    {
        None,
        Alpha,
        Scaling,
        Animation,
        AnchorMin,
        AnchorPositions,
        AnchorMax,
        AnchorPositionX,
        AnchorPositionY
    }
    
    [Serializable]
    public class UIAnimationStep : BaseAnimationStep
    {
        public AnimationBaseParams<float> Alpha;
        public AnimationBaseParams<Vector3> Scaling;
        public UIAnimationParamsAnimator Animation;
        public AnimationBaseParams<Vector2> AnchorMin;
        public AnimationBaseParams<Vector2> AnchorMax;
        public AnimationBaseParams<Vector2> AnchorPositions;
        public AnimationBaseParams<float> AnchorPositionX;
        public AnimationBaseParams<float> AnchorPositionY;
    }
}


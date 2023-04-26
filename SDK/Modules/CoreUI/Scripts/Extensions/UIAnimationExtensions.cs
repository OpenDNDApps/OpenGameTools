using DG.Tweening;

namespace OGT
{
    public static class UIAnimationExtensions
    {
        public static void JoinByStepType(this Sequence sequence, ScriptableAnimationJoinType step, Tween tween)
        {
            switch (step)
            {
                default:
                case ScriptableAnimationJoinType.Join:
                    sequence.Join(tween);
                    break;
                case ScriptableAnimationJoinType.Append:
                    sequence.Append(tween);
                    break;
            }
        }
    }
}
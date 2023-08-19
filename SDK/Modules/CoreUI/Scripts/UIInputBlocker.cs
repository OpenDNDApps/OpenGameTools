namespace OGT
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    
    public class UIInputBlocker : UIItem, IPointerClickHandler
    {
        protected override void OnInit()
        {
            base.OnInit();
            
            RectTransform.localPosition = Vector3.zero;
            RectTransform.localScale = Vector3.one;
            RectTransform.SetAsFirstSibling();

            Window.OnShow += Show;
            Window.OnAnimatedShowStart += AnimatedShow;
            Window.OnHide += Hide;
            Window.OnAnimatedHideStart += AnimatedHide;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnInputBlockerClick();
        }

        private void OnInputBlockerClick()
        {
            switch (Window.InputBlocker.ClickBehaviour)
            {
                case InputBlockClickBehaviour.Hide:
                    Window.Hide();
                    return;
                case InputBlockClickBehaviour.AnimatedHide:
                    Window.AnimatedHide();
                    return;
                case InputBlockClickBehaviour.Show:
                    Window.Show();
                    return;
                case InputBlockClickBehaviour.AnimatedShow:
                    Window.AnimatedShow();
                    return;
                default:
                case InputBlockClickBehaviour.None:
                    break;
            }
        }
    }
}
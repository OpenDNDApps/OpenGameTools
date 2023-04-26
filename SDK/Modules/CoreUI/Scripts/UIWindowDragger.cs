using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OGT
{
    public class UIWindowDragger : UIItemBase, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image m_dragSafeSpace;
        
        private bool m_isDragging;
        private Vector2 m_startingPosition;
        private Vector2 m_desiredPosition;
        private Vector2 m_pointerOffset;

        protected override void Awake()
        {
            base.Awake();
            if (m_dragSafeSpace != null)
            {
                m_dragSafeSpace.gameObject.SetActive(false);
            }
        }

        public void Update()
        {
            if (!m_isDragging)
                return;
            
            Window.RectTransform.localPosition = m_desiredPosition;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(Window.RectTransform, eventData.position, eventData.pressEventCamera, out m_pointerOffset);
            m_desiredPosition = Window.RectTransform.localPosition;
            m_isDragging = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            m_isDragging = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!m_isDragging)
                return;
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(Window.Canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out Vector2 deltaPosition);
            m_desiredPosition = deltaPosition - m_pointerOffset;
        }
    }
}

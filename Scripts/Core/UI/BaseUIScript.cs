using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Anvil3D
{
    public class BaseUIScript : AnvilMonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {

        #region Base Config

        private const string kBaseUIConfigGroupKey = "Base UI Settings";
        private const int kBaseUIConfigGroupSort = -98;

        private const string kShowHideButtonKey = "ShowHideButtons";
        
        [SerializeField, FoldoutGroup(kBaseUIConfigGroupKey, order:kBaseUIConfigGroupSort)]
        protected bool m_hideOnAwake;
        
        [SerializeField, FoldoutGroup(kBaseUIConfigGroupKey, order:kBaseUIConfigGroupSort)]
        protected CanvasGroup m_rootCanvasGroup;

        [SerializeField, FoldoutGroup(kBaseUIConfigGroupKey, order:kBaseUIConfigGroupSort), ReadOnly]
        protected RectTransform m_rectTransform;
        
        public CanvasGroup RootCanvasGroup => m_rootCanvasGroup;
        public RectTransform RectTransform => m_rectTransform;
        
        #endregion

        #region Drag Options
        
        private const string kDragOptionsGroupKey = "Is Draggable";
        private const string kDragOptionsGroupVariable = "m_isDraggable";
        private const int kDragOptionsGroupSort = 201;

        [Space]
        [ToggleGroup(kDragOptionsGroupVariable, groupTitle:kDragOptionsGroupKey, order:kDragOptionsGroupSort)]
        [SerializeField] private bool m_isDraggable = false;
        
        [ToggleGroup(kDragOptionsGroupVariable, groupTitle:kDragOptionsGroupKey, order:kDragOptionsGroupSort)]
        [SerializeField] private bool m_moveToFrontOnDrag = false;

        [ToggleGroup(kDragOptionsGroupVariable, groupTitle:kDragOptionsGroupKey, order:kDragOptionsGroupSort)]
        [SerializeField] private UnityEvent m_onDragBegins;
        
        [ToggleGroup(kDragOptionsGroupVariable, groupTitle:kDragOptionsGroupKey, order:kDragOptionsGroupSort)]
        [SerializeField] private UnityEvent m_onDrag;
        
        [ToggleGroup(kDragOptionsGroupVariable, groupTitle:kDragOptionsGroupKey, order:kDragOptionsGroupSort)]
        [SerializeField] private UnityEvent m_onDragEnds;

        #endregion

        protected virtual void Awake()
        {
            if (m_hideOnAwake)
            {
                Hide();
            }
            m_rectTransform = transform as RectTransform;
        }

        protected virtual void Start()
        {
            
        }

        [Button(ButtonSizes.Medium), ButtonGroup(kShowHideButtonKey)]
        public virtual void Show()
        {
            if (!m_rootCanvasGroup.isActiveAndEnabled)
            {
                m_rootCanvasGroup.DOFade(0f, 0f);
                m_rootCanvasGroup.gameObject.SetActive(true);
            }
            m_rootCanvasGroup.DOFade(1f, 0f);
        }
        
        [Button(ButtonSizes.Medium), ButtonGroup(kShowHideButtonKey)]
        public virtual void Hide()
        {
            m_rootCanvasGroup.DOFade(0f, 0f).OnComplete(() => m_rootCanvasGroup.gameObject.SetActive(false));
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!m_isDraggable)
            {
                return;
            }
            
            m_onDrag?.Invoke();
            
            var anchoredPosition = m_rectTransform.anchoredPosition;
            var newPosition = new Vector2(anchoredPosition.x + eventData.delta.x, anchoredPosition.y + eventData.delta.y);
            m_rectTransform.anchoredPosition = newPosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!m_isDraggable)
            {
                return;
            }

            if (m_moveToFrontOnDrag)
            {
                m_rectTransform.SetAsLastSibling();
            }
            
            m_onDragBegins?.Invoke();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!m_isDraggable)
            {
                return;
            }
            
            m_onDragEnds?.Invoke();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Anvil3D
{
    public class BaseUIScript : AnvilMonoBehaviour
    {
        [SerializeField] protected CanvasGroup m_rootCanvasGroup;
        public CanvasGroup RootCanvasGroup => m_rootCanvasGroup;

        [SerializeField] private bool m_hideOnAwake;
        

        protected virtual void Awake()
        {
            if (m_hideOnAwake)
            {
                Hide();
            }
        }

        protected virtual void Start()
        {
            
        }

        [Button]
        public virtual void Show()
        {
            if (!m_rootCanvasGroup.isActiveAndEnabled)
            {
                m_rootCanvasGroup.DOFade(0f, 0f);
                m_rootCanvasGroup.gameObject.SetActive(true);
            }
            m_rootCanvasGroup.DOFade(1f, 0f);
        }
        
        [Button]
        public virtual void Hide()
        {
            m_rootCanvasGroup.DOFade(0f, 0f).OnComplete(() => m_rootCanvasGroup.gameObject.SetActive(false));
        }
    }
}
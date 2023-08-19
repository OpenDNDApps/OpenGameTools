using System.Collections;
using UnityEngine;
using TMPro;

namespace OGT
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextLinkObserver : BaseBehaviour
    {
        private int m_intersectedLinkID = -1;
        private bool m_ready;

        private TMP_Text m_tmp;

        private IEnumerator Start()
        {
            m_tmp = GetComponent<TMP_Text>();
            
            yield return new WaitUntil(() => UIRuntime.IsReady);
            m_ready = true;
        }

        private void LateUpdate()
        {
            if (!m_ready)
                return;
            
            HandleLinkIntersectionLogic();
        }

        private void HandleLinkIntersectionLogic()
        {
            if (m_tmp.textInfo.linkCount == 0)
                return;

            if (Application.isMobilePlatform || Application.isConsolePlatform)
                return;
            
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(m_tmp, Input.mousePosition, UIRuntime.UICamera);

            if (m_intersectedLinkID == -1 && linkIndex == -1)
                return;

            if (m_tmp.textInfo.linkInfo.Length <= linkIndex)
                return;

            TMP_LinkInfo linkInfo = m_tmp.textInfo.linkInfo[linkIndex];
            
            if (linkIndex == -1 && m_intersectedLinkID != -1)
            {
                UIRuntime.NotifyOnLinkPointerExit(m_tmp.rectTransform, linkInfo);
                m_intersectedLinkID = -1;
            }

            if (linkIndex == -1)
                return;

            if (linkIndex != m_intersectedLinkID)
            {
                UIRuntime.NotifyOnLinkPointerEnter(m_tmp.rectTransform, linkInfo);
                m_intersectedLinkID = linkIndex;
                return;
            }
            
            UIRuntime.NotifyOnLinkPointerStay(m_tmp.rectTransform, linkInfo);
        }
    }
}

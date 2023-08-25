using UnityEngine;

namespace OGT
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] protected Camera m_worldCamera;
        [SerializeField] protected Camera m_uiCamera;
        
        protected virtual void Awake()
        {
            GameResources.Runtime.MainCameraController = this;
            GameRuntime.WorldCamera = m_worldCamera;
            UIRuntime.UICamera = m_uiCamera;
        }
    }
}

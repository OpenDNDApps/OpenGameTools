using UnityEngine;

namespace OGT
{
    public class BaseGameCharacterController : MonoBehaviour
    {
        protected virtual Vector3 m_gravity => Physics.gravity;
        protected virtual Vector3 m_gravityDelta => m_gravity * Time.deltaTime;

        protected virtual Transform m_origin => transform;
        protected virtual Vector3 m_originPosition => m_origin.position;
        protected virtual Quaternion m_targetRotation
        {
            get => m_origin.rotation;
            set => m_origin.rotation = value;
        }
        
        protected virtual Camera m_camera => GameRuntime.WorldCamera;

        protected virtual void PrepInputs()
        {
            
        }
    }
}


using UnityEngine;
using UnityEngine.Events;

namespace VGDevs
{
	public class VGDevsScriptableObject : ScriptableObject
	{
		#region ID / IIdentifiable Section
		
		[SerializeField] protected int m_id;
		
		public int ID => m_id;
		
		#endregion
		
		#region OnChange Section

		[SerializeField] protected bool m_resetInRuntime = true;

		[SerializeField] protected GameEvent m_onChangeScriptableObject;
		[SerializeField] protected UnityEvent m_onChangeEvent;
		[SerializeField] protected UnityAction m_onChangeAction;

		public virtual void OnChange()
		{
			if (m_onChangeScriptableObject != null)
			{
				m_onChangeScriptableObject.Raise();
			}
			m_onChangeEvent?.Invoke();
			m_onChangeAction?.Invoke();
		}
		
		#endregion
	}
}
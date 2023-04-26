using UnityEngine;
using UnityEngine.Events;

namespace OGT
{
	public class ScriptableVariableBase : BaseScriptableObject, ISerializationCallbackReceiver
	{
		[Header("Settings")]
		[SerializeField] protected bool m_resetAfterRuntime = true;
		
		#region ID / IIdentifiable Section
		
		[SerializeField] protected bool m_hasId = false;
		[ConditionalHide("m_hasId", true), SerializeField] protected int m_id;
		
		public int ID => m_id;
		
		#endregion
		
		#region OnChange Section
		
		[Header("OnChange")]
		[SerializeField] protected bool m_changeable = false;
		[ConditionalHide("m_changeable", true), SerializeField] protected GameEvent m_onChangeScriptableObject;
		[ConditionalHide("m_changeable", true), SerializeField] protected UnityEvent m_onChangeEvent;
		[ConditionalHide("m_changeable", true), SerializeField] protected UnityAction m_onChangeAction;

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

		public void OnBeforeSerialize() { }

		public void OnAfterDeserialize()
		{
			if(m_resetAfterRuntime)
				OnResetAfterRuntime();
		}

		public virtual void OnResetAfterRuntime() { }
	}
}
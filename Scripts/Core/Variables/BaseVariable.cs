using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anvil3D
{
	//[CreateAssetMenu(menuName = Anvil3D.kCreateMenuPrefixName + "Variables/BaseVariable")]
	public abstract class BaseVariable<T> : AnvilScriptableObject, ISerializationCallbackReceiver
	{
		[SerializeField] protected T m_value = default;
		[System.NonSerialized] protected T m_initValue = default;

		public virtual T Value
		{
			get
			{
				return m_value;
			}
			set
			{
				m_value = value;
				OnChange();
			}
		}

		public void OnAfterDeserialize()
		{
			if(m_resetInRuntime)
				m_value = m_initValue;
		}

		public void OnBeforeSerialize()	{}
	}
}
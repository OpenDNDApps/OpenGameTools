using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anvil3D
{
	//[CreateAssetMenu(menuName = "Anvil3D/Variables/BaseVariable")]
	public abstract class BaseVariable<T> : AnvilScriptableObject, ISerializationCallbackReceiver
	{
		[SerializeField]
		protected T _value = default;
		[System.NonSerialized]
		protected T initValue = default;

		public virtual T Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
				OnChange();
			}
		}

		public void OnAfterDeserialize()
		{
			if(m_resetInRuntime)
				_value = initValue;
		}

		public void OnBeforeSerialize()	{}
	}
}
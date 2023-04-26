
using UnityEngine;

namespace OGT
{
	public class PlayerController : ScriptableVariableBaseMono
	{
		public PlayerData m_data;

		public void Build(PlayerData newData = null)
		{
			if (newData != null)
			{
				m_data = newData;
			}
			
			if (m_data == null)
			{
				Debug.LogError($"Data '{this.name}' not found - Build()", this);
				return;
			}
			
			m_data.Instance = this;
		}
	}
}
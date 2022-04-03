
using UnityEngine;

namespace VGDevs
{
	public class PlayerController : VGDevsMonoBehaviour
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
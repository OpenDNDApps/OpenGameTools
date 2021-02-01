using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;

namespace Anvil3D
{
	public class WeaponWorldItem : AnvilMonoBehaviour, IDataBuildable<WeaponData>
	{
		[Header("3D Elements")]
		[SerializeField] private GameObject m_root;
		[SerializeField] private Transform m_3DPivot;
		[ReadOnly, SerializeField] private Transform m_body;
		
		[Header("3D UI")]
		[SerializeField] private Canvas m_UICanvas;
		[SerializeField] private CanvasGroup m_UIPivot;
		[SerializeField] private TextMeshProUGUI m_title;
		[SerializeField] private TextMeshProUGUI m_damage;
		[SerializeField] private TextMeshProUGUI m_durability;

		[Header("Data")]
		[SerializeField] private WeaponData m_data;
		
		public WeaponData Data => m_data;

		
		
		[PropertySpace, Button]
		public void Build(WeaponData newData = null)
		{
			if (newData != null)
			{
				m_data = newData;
			}
			
			if(m_data == null)
			{
				Debug.LogError($"Data '{this.name}' not found - Build()", this);
				return;
			}

			m_title.text = $"{m_data.Title}";
			m_damage.text = $"{m_data.Damage.Value}";
			m_durability.text = $"{m_data.Durability}";

			SpawnBody();
		}

		public void SpawnBody()
		{
			if (m_data == null)
			{
				Debug.LogError($"Data '{this.name}' not found - SpawnBody()", this);
				return;
			}

			if (m_body != null)
			{
				DestroyImmediate(m_body.gameObject);
				m_body = null;
			}

			m_body = Instantiate<Transform>(m_data.Body, m_3DPivot);
		}
	}
}
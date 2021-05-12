using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VGDevs
{
	public class WeaponUIItem : VGDevsMonoBehaviour, IDataBuildable<WeaponData>
	{
		// UI
		[Header("UI")]
		[SerializeField] private CanvasGroup m_rootPivot;
		[SerializeField] private TextMeshProUGUI m_title;
		[SerializeField] private Image m_icon;
		[SerializeField] private TextMeshProUGUI m_damage;
		[SerializeField] private TextMeshProUGUI m_durability;
		
		// Data
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
			
			if (m_data == null)
			{
				Debug.LogError($"Data '{this.name}' not found - Build()", this);
				return;
			}

			m_title.text = $"{m_data.Title}";
			m_damage.text = $"{m_data.Damage.Value}";
			m_durability.text = $"{m_data.Durability}";

			m_icon.overrideSprite = m_data.Art;
		}
	}
}
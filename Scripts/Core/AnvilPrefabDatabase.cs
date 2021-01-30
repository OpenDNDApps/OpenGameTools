using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Anvil3D
{
	[CreateAssetMenu(fileName = Anvil3D.kPrefabsAccessFileName, menuName = Anvil3D.kCreateMenuPrefixName + "Base/PrefabDatabase")]
	public class AnvilPrefabDatabase : ScriptableObject
	{
		/// <summary>
		/// 
		/// Usage 1: Direct usage
		///  
		///		public void MyCustomMethod() {
		///			Instantiate(Anvil3D.Prefabs.PrefabVariableName);
		///		}
		///		
		/// Usage 2 (recommended): Add the instance as a private variable
		///  
		///		[SerializeField] private WeaponUIItem m_prefab => Anvil3D.Prefabs.PrefabVariableName;
		/// 
		///		public void MyCustomMethod() {
		///			Instantiate(m_prefab);
		///		}
		/// 
		/// </summary>

		public PlayerController m_playerController;

		// Usage Example
		[PropertySpace(10), Title("Examples")]
		public WeaponUIItem WeaponUIItem;
		public WeaponWorldItem WeaponWorldItem;
	}
}
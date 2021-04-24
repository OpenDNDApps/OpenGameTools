using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace VGDevs
{
	[CreateAssetMenu(fileName = VGDevs.kPrefabsAccessFileName, menuName = VGDevs.kCreateMenuPrefixName + "Base/PrefabDatabase")]
	public class VGDevsPrefabDatabase : ScriptableObject
	{
		/// <summary>
		/// 
		/// Usage 1: Direct usage
		///  
		///		public void MyCustomMethod() {
		///			Instantiate(VGDevs.Prefabs.PrefabVariableName);
		///		}
		///		
		/// Usage 2 (recommended): Add the instance as a private variable
		///  
		///		[SerializeField] private WeaponUIItem m_prefab => VGDevs.Prefabs.PrefabVariableName;
		/// 
		///		public void MyCustomMethod() {
		///			Instantiate(m_prefab);
		///		}
		/// 
		/// </summary>

		public ExamplePlayerController m_examplePlayerController;

		// Usage Example
		[PropertySpace(10), Title("Examples")]
		public WeaponUIItem WeaponUIItem;
		public WeaponWorldItem WeaponWorldItem;
	}
}
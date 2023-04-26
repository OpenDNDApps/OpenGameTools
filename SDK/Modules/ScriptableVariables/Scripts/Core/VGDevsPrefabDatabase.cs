using System.Collections.Generic;
using UnityEngine;

namespace VGDevs
{
	[CreateAssetMenu(fileName = VGDevs.kPrefabsAccessFileName, menuName = VGDevs.kCreateMenuPrefixName + "Base/PrefabDatabase")]
	public class VGDevsPrefabDatabase : ScriptableObject
	{
		// Definition Example
		
		// public ExamplePlayerController m_examplePlayerController;
		// public WeaponUIItem WeaponUIItem;
		// public WeaponWorldItem WeaponWorldItem;
		
		
		// Remote (in another script) Usage Example
		
		// Usage 1: Direct usage
		// 
		//		public void MyCustomMethod() {
		//			Instantiate(VGDevs.Prefabs.PrefabVariableName);
		//		}
		//		
		// Usage 2: Forward as a private variable
		//  
		//		[SerializeField] private WeaponUIItem m_prefab => VGDevs.Prefabs.PrefabVariableName;
		// 
		//		public void MyCustomMethod() {
		//			Instantiate(m_prefab);
		//		}
	}
}
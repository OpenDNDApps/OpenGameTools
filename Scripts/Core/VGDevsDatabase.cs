using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGDevs
{
	[CreateAssetMenu(fileName = VGDevs.kMainDatabaseFileName, menuName = VGDevs.kCreateMenuPrefixName + "Base/Database")]
	public class VGDevsDatabase : VGDevsScriptableObject
	{
		public DamageModifiers DamageModifiers;
		
		public ExamplePlayerData m_examplePlayerData;

		public ItemDataCollection m_allItemDefinitionsInProject;

		public WeaponWorldItemCollection AllWeaponWorldItemsInGameBuilt;
		public WeaponUIItemCollection AllWeaponUIItemsInGameBuilt;
	}
}
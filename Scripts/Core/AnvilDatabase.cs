using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anvil3D
{
	[CreateAssetMenu(fileName = Anvil3D.kMainDatabaseFileName, menuName = Anvil3D.kCreateMenuPrefixName + "Base/Database")]
	public class AnvilDatabase : AnvilScriptableObject
	{
		public DamageModifiers DamageModifiers;
		
		public ExamplePlayerData m_examplePlayerData;

		public WeaponDataCollection AllWeaponDatasInProject;

		public WeaponWorldItemCollection AllWeaponWorldItemsInGameBuilt;
		public WeaponUIItemCollection AllWeaponUIItemsInGameBuilt;
	}
}
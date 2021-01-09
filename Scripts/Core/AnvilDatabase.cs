using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anvil3D
{
	[CreateAssetMenu(fileName = Anvil3D.kMainDatabaseFileName, menuName = Anvil3D.kMainDatabaseMenuName)]
	public class AnvilDatabase : AnvilScriptableObject
	{
		public PlayerData PlayerData;

		public WeaponDataCollection AllWeaponDatasInProject;

		public WeaponWorldItemCollection AllWeaponWorldItemsInGameBuilt;
		public WeaponUIItemCollection AllWeaponUIItemsInGameBuilt;
	}
}
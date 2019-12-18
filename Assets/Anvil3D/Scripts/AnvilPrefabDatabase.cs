using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Anvil3D
{
	[CreateAssetMenu(menuName = "Anvil3D/Base/PrefabDatabase")]
	public class AnvilPrefabDatabase : ScriptableObject
	{
		/// <summary>
		/// Usage 1: Direct usage
		///  
		///		public void MyCustomMethod() {
		///			Debug.Log(AnvilPrefabDatabase.Instance.version);
		///		}
		///		
		/// Usage 2: Add the instance as a private variable
		///  
		///		private AnvilPrefabDatabase Settings => AnvilPrefabDatabase.Instance;
		/// 
		///		public void MyCustomMethod() {
		///			Debug.Log(Settings.version);
		///		}
		///		
		/// Usage 2: Use Anvil shortcut
		///  
		///		public void MyCustomMethod() {
		///			Debug.Log(Anvil.Prefabs.version);
		///		}
		/// </summary>

		public Player player;


		// EXAMPLES
		[PropertySpace(10), Title("Examples")]
		public WeaponUIItem weaponUIItem;
		public WeaponWorldItem weaponWorldItem;
	}
}
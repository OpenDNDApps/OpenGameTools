using System.Collections.Generic;
using UnityEngine;

namespace Anvil3D
{
	[CreateAssetMenu(fileName = "AnvilGameSettings", menuName = "Anvil3D/Base/GameSettings")]
	public class AnvilGameSettings : ScriptableObject
	{
		/// <summary>
		/// Usage 1: Direct usage
		///  
		///		public void MyCustomMethod() {
		///			Debug.Log(AnvilGameSettings.Instance.version);
		///		}
		///		
		/// Usage 2: Add the instance as a private variable
		///  
		///		private AnvilGameSettings Settings => AnvilGameSettings.Instance;
		/// 
		///		public void MyCustomMethod() {
		///			Debug.Log(Settings.version);
		///		}
		///		
		/// Usage 2: Use Anvil shortcut
		///  
		///		public void MyCustomMethod() {
		///			Debug.Log(Anvil.Settings.version);
		///		}
		/// </summary>

		public string version = "v0.0.1a";
		public string versionPref = "game_version";

		public int build = 0010;
		public string buildPref = "game_build";

		public DamageModifiers damageModifiers;
	}
}
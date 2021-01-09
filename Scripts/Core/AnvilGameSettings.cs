using System.Collections.Generic;
using UnityEngine;

namespace Anvil3D
{
	[CreateAssetMenu(fileName = Anvil3D.kGamePropertiesFileName, menuName = Anvil3D.kGamePropertiesMenuName)]
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

		public string Version = "v0.0.1a";
		public string VersionPref = "game_version";

		public int Build = 0010;
		public string BuildPref = "game_build";

		public DamageModifiers DamageModifiers;
	}
}
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VGDevs
{
	[CreateAssetMenu(fileName = VGDevs.kGameSettingsFileName, menuName = VGDevs.kCreateMenuPrefixName + "Base/GameSettings")]
	public class VGDevsGameSettings : ScriptableObject
	{
		/// <summary>
		/// Usage 1: Direct usage
		///  
		///		public void MyCustomMethod() {
		///			Debug.Log(VGDevsGameSettings.Instance.version);
		///		}
		///		
		/// Usage 2: Add the instance as a private variable
		///  
		///		private VGDevsGameSettings Settings => VGDevsGameSettings.Instance;
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

		[ValueDropdown("@VGDevs.EnvironmentDropdownValues")]
		public string Environment = "Dev1";
		
		[NonSerialized][ShowInInspector]
		public string AssignmentID = "";
		
		public string Version = "v0.0.1";
		public string VersionPref = "game_version";
		

		public int Build = 0010;
		public string BuildPref = "game_build";
	}
}
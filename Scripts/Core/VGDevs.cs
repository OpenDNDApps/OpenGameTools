using Sirenix.OdinInspector;

namespace VGDevs
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using UnityEngine;

	public static class VGDevs
	{
		#region Script settings

		/// <summary>
		/// Used by EventsManager.
		/// </summary>

		public const string kCreateMenuPrefixName = "Data/";

		public const string kGameEventPrefix = "MyGameEvent_";

		public const string kGameSettingsFileName = "GameSettings";
		public const string kMainDatabaseFileName = "ScritableDatabase_Main";
		public const string kPrefabsAccessFileName = "ScritableDatabase_GamePrefabs";
		
		#endregion
		
		#region Base scripts editor settings

		/// Has ID Editor Settings.
		public const int kIDSortValue = -99;
		public const string kIDGroupKeyVariable = "m_hasID";
		public const string kIDGroupTitle = "Has ID";

		/// On Change Editor Settings.
		public const int kOnChangeOrder = 999;
		public const string kOnChangeGroupKeyVariable = "m_onChangeToggle";
		public const string kOnChangeGroupTitle = "On Change Events";
		public const string kOnChangeButtonTitle = "Trigger OnChange";

		#endregion

		#region RemoteConfig Settings

		public static ValueDropdownList<string> EnvironmentDropdownValues = new ValueDropdownList<string>()
		{
			"Production", "Staging", "Dev1", "Dev2", "Testing", "QA"
		};
        
		public struct UserAttributes {
			public string UserID => PlayerPrefs.GetString("UserID", "Unknown");
			public string DeviceID => SystemInfo.deviceUniqueIdentifier;
		}

		public struct AppAttributes {
			public int Build => VGDevs.Settings.Build;
			public string Version => VGDevs.Settings.Version;
			public string Env => VGDevs.Settings.Environment;
		}
		
		#endregion
		

		#region Static References

		private static VGDevsGameSettings m_gameSettings;
		public static VGDevsGameSettings Settings
		{
			get
			{
				if (m_gameSettings == null)
					m_gameSettings = (VGDevsGameSettings)Resources.Load(VGDevs.kGameSettingsFileName, typeof(VGDevsGameSettings));
				if (m_gameSettings == null)
					Debug.LogError($"Asset '{VGDevs.kGameSettingsFileName}' not found.");
				return m_gameSettings;
			}
		}

		private static VGDevsDatabase m_dataBase;
		public static VGDevsDatabase Database
		{
			get
			{
				if (m_dataBase == null)
					m_dataBase = (VGDevsDatabase)Resources.Load(VGDevs.kMainDatabaseFileName, typeof(VGDevsDatabase));
				if (m_dataBase == null)
					Debug.LogError($"Asset '{VGDevs.kMainDatabaseFileName}' not found.");
				return m_dataBase;
			}
		}

		private static VGDevsPrefabDatabase m_prefabDatabase;
		public static VGDevsPrefabDatabase Prefabs
		{
			get
			{
				if (m_prefabDatabase == null)
					m_prefabDatabase = (VGDevsPrefabDatabase)Resources.Load(VGDevs.kPrefabsAccessFileName, typeof(VGDevsPrefabDatabase));
				if (m_prefabDatabase == null)
					Debug.LogError($"Asset '{VGDevs.kPrefabsAccessFileName}' not found.");
				return m_prefabDatabase;
			}
		}

		#endregion
	}
}
#if UNITY_EDITOR
#region Editor top menu methods
namespace VGDevs
{
	using UnityEditor;
	public static class VGDevsEditor
	{
		private const string kMenuPath = "Tools/VGDevs/";
		
		[MenuItem(kMenuPath + "Select Game Settings")]
		private static void SelectGameProperties()
		{
			Selection.activeObject = VGDevs.Settings;
			EditorGUIUtility.PingObject(Selection.activeObject);
		}

		[MenuItem(kMenuPath + "Select Database Access")]
		private static void SelectDataAccess()
		{
			Selection.activeObject = VGDevs.Database;
			EditorGUIUtility.PingObject(UnityEditor.Selection.activeObject);
		}
		[MenuItem(kMenuPath + "Select Prefabs Access")]
		private static void SelectPrefabsAccess()
		{
			Selection.activeObject = VGDevs.Prefabs;
			EditorGUIUtility.PingObject(Selection.activeObject);
		}
	}
}
#endregion
#endif

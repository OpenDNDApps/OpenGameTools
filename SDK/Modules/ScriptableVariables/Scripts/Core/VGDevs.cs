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

		public const string kPluginName = "VGDevs";

		public const string kCreateMenuPrefixName = kPluginName + "/Data/";
		public const string kCreateMenuPrefixNameVariables = kCreateMenuPrefixName + "Variables/";
		public const string kCreateMenuPrefixNameGame = kCreateMenuPrefixName + "Game/";
		public const string kCreateMenuPrefixNameEvents = kCreateMenuPrefixName + "Events/";

		public const string kGameEventPrefix = "GE_";

		public const string kGameSettingsFileName = "GameSettings";
		public const string kMainDatabaseFileName = "ScritableDatabase_Main";
		public const string kPrefabsAccessFileName = "ScritableDatabase_GamePrefabs";
		
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
		private const string kMenuPath = "Tools/" + VGDevs.kPluginName + "/";
		
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

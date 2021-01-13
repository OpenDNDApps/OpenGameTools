namespace Anvil3D
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using UnityEngine;
	
	public static class Anvil3D
	{
		#region Script settings

		/// <summary>
		/// Used by EventsManager.
		/// </summary>

		public const string kCreateMenuPrefixName = "Data/";
		
		public const string kGameEventPrefix = "Anvil_";

		public const string kAPIBaseURL = "https://castle.myl.cl/";

		public const string kGameSettingsFileName = "AnvilGameSettings";
		public const string kMainDatabaseFileName = "AnvilDatabase";
		public const string kPrefabsAccessFileName = "AnvilPrefabDatabase";
		
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


		#region Static References

		private static AnvilGameSettings m_gameSettings;
		public static AnvilGameSettings Settings
		{
			get
			{
				if (m_gameSettings == null)
					m_gameSettings = (AnvilGameSettings)Resources.Load(Anvil3D.kGameSettingsFileName, typeof(AnvilGameSettings));
				if (m_gameSettings == null)
					Debug.LogError($"Asset '{Anvil3D.kGameSettingsFileName}' not found.");
				return m_gameSettings;
			}
		}

		private static AnvilDatabase m_dataBase;
		public static AnvilDatabase Database
		{
			get
			{
				if (m_dataBase == null)
					m_dataBase = (AnvilDatabase)Resources.Load(Anvil3D.kMainDatabaseFileName, typeof(AnvilDatabase));
				if (m_dataBase == null)
					Debug.LogError($"Asset '{Anvil3D.kMainDatabaseFileName}' not found.");
				return m_dataBase;
			}
		}

		private static AnvilPrefabDatabase m_prefabDatabase;
		public static AnvilPrefabDatabase Prefabs
		{
			get
			{
				if (m_prefabDatabase == null)
					m_prefabDatabase = (AnvilPrefabDatabase)Resources.Load(Anvil3D.kPrefabsAccessFileName, typeof(AnvilPrefabDatabase));
				if (m_prefabDatabase == null)
					Debug.LogError($"Asset '{Anvil3D.kPrefabsAccessFileName}' not found.");
				return m_prefabDatabase;
			}
		}

		#endregion
	}
}
#if UNITY_EDITOR
#region Editor top menu methods
namespace Anvil3D
{
	using UnityEditor;
	public static class AnvilEditor
	{
		private const string kMenuPath = "Tools/Anvil3D/";
		
		[MenuItem(kMenuPath + "Select Game Settings")]
		private static void SelectGameProperties()
		{
			Selection.activeObject = Anvil3D.Settings;
			EditorGUIUtility.PingObject(Selection.activeObject);
		}

		[MenuItem(kMenuPath + "Select Database Access")]
		private static void SelectDataAccess()
		{
			Selection.activeObject = Anvil3D.Database;
			EditorGUIUtility.PingObject(UnityEditor.Selection.activeObject);
		}
		[MenuItem(kMenuPath + "Select Prefabs Access")]
		private static void SelectPrefabsAccess()
		{
			Selection.activeObject = Anvil3D.Prefabs;
			EditorGUIUtility.PingObject(Selection.activeObject);
		}
	}
}
#endregion
#endif

namespace Anvil3D
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using UnityEngine;

	public static class Anvil
	{
		#region Constants
		/// <summary>
		/// Used by EventsManager.
		/// </summary>
		public const string GAME_EVENT_PREFIX = "Anvil_";

		public const string API_BASE_URL = "https://castle.myl.cl/";

		public const string kGamePropertiesFileName = "AnvilGameSettings";
		public const string kMasterAccessFileName = "AnvilDatabase";
		public const string kPrefabsAccessFileName = "AnvilPrefabDatabase";
		
		public const int kIDPrefixINTValue = -99;
		#endregion


		#region Static References

		private static AnvilGameSettings _gameSettings;
		public static AnvilGameSettings Settings
		{
			get
			{
				if (_gameSettings == null)
					_gameSettings = (AnvilGameSettings)Resources.Load(Anvil.kGamePropertiesFileName, typeof(AnvilGameSettings));
				if (_gameSettings == null)
					Debug.LogError($"Asset '{Anvil.kGamePropertiesFileName}' not found.");
				return _gameSettings;
			}
		}

		private static AnvilDatabase _dataBase;
		public static AnvilDatabase Database
		{
			get
			{
				if (_dataBase == null)
					_dataBase = (AnvilDatabase)Resources.Load(Anvil.kMasterAccessFileName, typeof(AnvilDatabase));
				if (_dataBase == null)
					Debug.LogError($"Asset '{Anvil.kMasterAccessFileName}' not found.");
				return _dataBase;
			}
		}

		private static AnvilPrefabDatabase _prefabDatabase;
		public static AnvilPrefabDatabase Prefabs
		{
			get
			{
				if (_prefabDatabase == null)
					_prefabDatabase = (AnvilPrefabDatabase)Resources.Load(Anvil.kPrefabsAccessFileName, typeof(AnvilPrefabDatabase));
				if (_prefabDatabase == null)
					Debug.LogError($"Asset '{Anvil.kPrefabsAccessFileName}' not found.");
				return _prefabDatabase;
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
		public const string MenuPath = "Tools/Anvil3D/";
		[MenuItem(MenuPath + "Select Game Settings")]
		static void SelectGameProperties()
		{
			Selection.activeObject = Anvil.Settings;
			EditorGUIUtility.PingObject(Selection.activeObject);
		}

		[MenuItem(MenuPath + "Select Database Access")]
		static void SelectDataAccess()
		{
			Selection.activeObject = Anvil.Database;
			EditorGUIUtility.PingObject(UnityEditor.Selection.activeObject);
		}
		[MenuItem(MenuPath + "Select Prefabs Access")]
		static void SelectPrefabsAccess()
		{
			Selection.activeObject = Anvil.Prefabs;
			EditorGUIUtility.PingObject(Selection.activeObject);
		}
	}
}
#endregion
#endif

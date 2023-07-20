namespace OGT
{
    public static partial class GameResources
    {
        public const string kSettingsFileName = "GameSettings";
        
        private static GameSettingsCollection m_settings;
        public static GameSettingsCollection Settings => GetGameResource(ref m_settings, kSettingsFileName);
    }
}

#if UNITY_EDITOR
#region Editor top menu methods
namespace OGT
{
    using UnityEditor;
    public static partial class CoreEditor
    {
        [MenuItem(GameResources.kModuleMenuPath + "Select Settings")]
        private static void SelectGameSettings()
        {
            Selection.activeObject = GameResources.Settings;
            EditorGUIUtility.PingObject(UnityEditor.Selection.activeObject);
        }
    }
}
#endregion
#endif
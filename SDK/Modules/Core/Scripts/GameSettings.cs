namespace OGT
{
    public static partial class GameResources
    {
        public static GameSettingsCollection Settings => GetGameResource(ref m_settings);
        
        private static GameSettingsCollection m_settings;
    }
}

#if UNITY_EDITOR
#region Editor top menu methods
namespace OGT
{
    using UnityEditor;
    public static partial class CoreEditor
    {
        [MenuItem(OGTConstants.kModuleMenuPath + "Select ⚙️ Settings")]
        private static void SelectGameSettings()
        {
            Selection.activeObject = GameResources.Settings;
            EditorGUIUtility.PingObject(Selection.activeObject);
        }
    }
}
#endregion
#endif
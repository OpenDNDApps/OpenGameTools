namespace OGT
{
    public static partial class GameResources
    {
        public static UIRuntime UIRuntime => UIRuntime.Instance;
        
        public const string kUIFileName = "UIGameResources";
        public const string kCreateUIGameObjectMenuPath = "GameObject/UI/" + OGTConstants.kPluginName + "/";
        
        private static UIResourcesCollection m_ui;
        public static UIResourcesCollection UI => GetGameResource(ref m_ui);
    }
}

#if UNITY_EDITOR
#region Editor top menu methods
namespace OGT
{
    using UnityEditor;
    public static partial class CoreEditor
    {
        [MenuItem(OGTConstants.kModuleMenuPath + "Select 📚 UI")]
        private static void SelectGameUIResources()
        {
            Selection.activeObject = GameResources.UI;
            EditorGUIUtility.PingObject(Selection.activeObject);
        }
    }
}
#endregion
#endif

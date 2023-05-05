using UnityEngine;

namespace OGT
{
    public static partial class GameResources
    {
        public static GameRuntime Runtime => GameRuntime.Instance;
        
        // TODO: Do not merge this to OGT.
        public const string kPluginName = "OGT";
        
        public const string kCreateComponentUIPath = kPluginName + "/UI/";
        public const string kCreateMenuPrefixNameResources = kPluginName + "/Base Collections/";
        public const string kCreateMenuPrefixModules = kPluginName + "/Modules/";
        public const string kCreateGameObjectMenuPath = "GameObject/" + kPluginName + "/";
        public const string kCreateUIMenuPath = "GameObject/" + kPluginName + "/UI/";

        public const string kGeneralFileName = "GameResources";
        
        private static GameResourcesCollection m_general;

        public static GameResourcesCollection General => GetGameResource(ref m_general, kGeneralFileName);

        private static T GetGameResource<T>(ref T localVariable, string filePath) where T : ScriptableObject
        {
            if (localVariable == default)
                localVariable = (T)Resources.Load(filePath, typeof(T));
            if (localVariable == default)
                Debug.LogError($"Asset '{filePath}' not found.");
            return localVariable as T;
        }
    }
}

#if UNITY_EDITOR
#region Editor top menu methods
namespace OGT
{
    using UnityEditor;
    public static partial class CoreEditor
    {
        private const string kMenuPath = GameResources.kPluginName + "/";
        private const string kModuleMenuPath = kMenuPath + "Module Resources/";

        private const string kDocumentationURL = "https://github.com/OpenDNDApps/OpenGameTools";
		
        [MenuItem(kModuleMenuPath + "Select General")]
        private static void SelectGameProperties()
        {
            Selection.activeObject = GameResources.General;
            EditorGUIUtility.PingObject(Selection.activeObject);
        }
		
        [MenuItem(kMenuPath + "Documentation")]
        private static void SelectDocumentation()
        {
            Application.OpenURL(kDocumentationURL);
        }
    }
}
#endregion
#endif

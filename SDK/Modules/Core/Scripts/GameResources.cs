using System;
using UnityEngine;

namespace OGT
{
    public static partial class GameResources
    {
        public static GameRuntime Runtime => GameRuntime.Instance;
        public static GameDependenciesCollection Dependencies => GetGameResource(ref m_dependencies);

        private static GameDependenciesCollection m_dependencies;

        private static T GetGameResource<T>(ref T localVariable) where T : ScriptableObject
        {
            if (localVariable != default)
                return localVariable;

            Type varType = typeof(T);
            localVariable = (T)Resources.Load(varType.Name, varType);
            if (localVariable == default)
                Debug.LogError($"Asset '{varType.Name}' not found in Resources Folder(s).");
            return localVariable;
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
		
        [MenuItem(OGTConstants.kModuleMenuPath + "Select 🛠️ Dependencies Collection")]
        private static void SelectGameProperties()
        {
            Selection.activeObject = GameResources.Dependencies;
            EditorGUIUtility.PingObject(Selection.activeObject);
        }
		
        [MenuItem(OGTConstants.kMenuPath + "📝 Documentation")]
        private static void SelectDocumentation()
        {
            Application.OpenURL(OGTConstants.kDocumentationURL);
        }
    }
}
#endregion
#endif

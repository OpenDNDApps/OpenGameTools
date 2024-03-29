﻿namespace OGT
{
    public static partial class GameResources
    {
        public const string kCreateMenuPrefixName = OGTConstants.kPluginName + "/ScriptableVariables/";
        public const string kCreateMenuPrefixNameVariables = kCreateMenuPrefixName + "Variables/";
        public const string kCreateMenuPrefixNameGame = kCreateMenuPrefixName + "Game/";
        public const string kCreateMenuPrefixNameEvents = kCreateMenuPrefixName + "Events/";
        public const string kGameEventPrefix = "GE_";
        
        public static ScriptableVariablesResourcesCollection ScriptableVariables => GetGameResource(ref m_scriptableVariables);
        
        
        private static ScriptableVariablesResourcesCollection m_scriptableVariables;
    }
}

#if UNITY_EDITOR
#region Editor top menu methods
namespace OGT
{
    using UnityEditor;
    public static partial class CoreEditor
    {
        [MenuItem(OGTConstants.kModuleMenuPath + "Select 📦 ScriptableVariables")]
        private static void SelectScriptableVariablesResources()
        {
            Selection.activeObject = GameResources.ScriptableVariables;
            EditorGUIUtility.PingObject(Selection.activeObject);
        }
    }
}
#endregion
#endif
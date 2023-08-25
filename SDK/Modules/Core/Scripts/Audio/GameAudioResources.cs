namespace OGT
{
    public static partial class GameResources
    {
        public static AudioRuntime AudioRuntime => AudioRuntime.Instance;
        public static GameAudioResourcesCollection Audio => GetGameResource(ref m_audio);
        
        private static GameAudioResourcesCollection m_audio;
    }
}

#if UNITY_EDITOR
#region Editor top menu methods
namespace OGT
{
    using UnityEditor;
    public static partial class CoreEditor
    {
        [MenuItem(OGTConstants.kModuleMenuPath + "Select 🎵 Audio")]
        private static void SelectGameAudioResources()
        {
            Selection.activeObject = GameResources.Audio;
            EditorGUIUtility.PingObject(Selection.activeObject);
        }
    }
}
#endregion
#endif
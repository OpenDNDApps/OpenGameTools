using System.Collections.Generic;
using UnityEngine;

namespace OGT
{
    [CreateAssetMenu(fileName = GameResources.kGameAudioFileName, menuName = GameResources.kCreateMenuPrefixNameResources + GameResources.kGameAudioFileName)]
    public partial class GameAudioResourcesCollection : BaseResourcesCollection
    {
        [SerializeField] private List<AudioClipDefinition> m_sounds = new List<AudioClipDefinition>();

        public bool TryGetAudioDefinition(string audioName, out AudioClipDefinition uiItem)
        {
            uiItem = null;
            if (string.IsNullOrEmpty(audioName)) 
                return false;
            
            foreach (AudioClipDefinition item in m_sounds)
            {
                if (!item)
                {
                    Debug.LogError($"Missing element detected in '{nameof(GameAudioResourcesCollection)}'.'{nameof(m_sounds)}'", this);
                    continue;
                }
                if (!item.name.Equals(audioName)) 
                    continue;
				
                uiItem = item;
                return true;
            }
            return false;
        }

        
        #if UNITY_EDITOR
        public void ManualValidate()
        {
            bool result = true;
            foreach (AudioClipDefinition clipDefinition in m_sounds)
            {
                result &= CheckDefinitionValidity(clipDefinition);
            }

            if (result)
            {
                Debug.Log("<color=green><b>✓ Success!</b></color> All audio assets are valid.");
            }
        }
        
        private bool CheckDefinitionValidity(AudioClipDefinition p_definition)
        {
            if (p_definition == null)
            {
                Debug.LogError($"There are Audio clip definition missing in the Sounds library.");
                return false;
            }
            if (p_definition.Clip == null)
            {
                Debug.LogError($"The Audio clip definition '{p_definition.name}' is missing its Clip.");
                return false;
            }

            string clipAssetPath = UnityEditor.AssetDatabase.GetAssetPath(p_definition.Clip);
            UnityEditor.AudioImporter clipAudioImporter = UnityEditor.AssetImporter.GetAtPath(clipAssetPath) as UnityEditor.AudioImporter;
            if (clipAudioImporter == null)
            {
                Debug.LogError($"The Audio clip definition '{p_definition.Clip.name}' needs an AudioImporter.");
                return false;
            }
            
            bool l_result = true;
            switch (p_definition.Layer)
            {
                case AudioLayer.UISFX:
                case AudioLayer.SoundFX:
                    if (clipAudioImporter.defaultSampleSettings.loadType != AudioClipLoadType.CompressedInMemory)
                    {
                        Debug.LogError($"The Audio clip in definition '{p_definition.name}' has a wrong Load Type, it needs to be CompressedInMemory.");
                        l_result = false;
                    }
                    break;
                case AudioLayer.Music:
                case AudioLayer.Ambient:
                    if (!clipAssetPath.StartsWith("Assets/Audio/"))
                    {
                        Debug.LogError($"The Audio clip in definition '{p_definition.name}' needs to be in the 'Assets/Audio/' folder. Currently: '{clipAssetPath}'");
                        l_result = false;
                    }
                    if (clipAudioImporter.defaultSampleSettings.loadType != AudioClipLoadType.Streaming)
                    {
                        Debug.LogError($"The Audio clip in definition '{p_definition.name}' has a wrong Load Type, it needs to be Streaming.");
                        l_result = false;
                        
                        // if (clipAudioImporter.preloadAudioData)
                        // {
                        //     Debug.LogError($"The Audio clip in definition '{p_definition.name}' has a wrong Preload Audio Data, it needs to be false.");
                        //     l_result = false;
                        // }
                    }
                    break;
                case AudioLayer.Default:
                    break;
            }
            
            if (clipAudioImporter.defaultSampleSettings.compressionFormat != AudioCompressionFormat.Vorbis)
            {
                Debug.LogError($"The Audio clip in definition '{p_definition.name}' has a wrong Compression Format, it needs to be Vorbis.");
                l_result = false;
            }
            
            if (clipAudioImporter.defaultSampleSettings.quality > 0.7f)
            {
                Debug.LogError($"The Audio clip in definition '{p_definition.name}' has a wrong Quality, it needs to be 0.7f or lower.");
                l_result = false;
            }

            return l_result;
        }
        #endif
    }
    
    
    #if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(GameAudioResourcesCollection))]
    public class AudioEssentialsEditor : UnityEditor.Editor
    {
        private GameAudioResourcesCollection m_target;
        
        private void OnEnable()
        {
            m_target = target as GameAudioResourcesCollection;
        }
        
        public override void OnInspectorGUI()
        {
            UnityEditor.EditorGUILayout.Space();
            UnityEditor.EditorGUI.BeginDisabledGroup(Application.isPlaying);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Validate Assets"))
            {
                m_target.ManualValidate();
            }
            GUILayout.EndHorizontal();
            UnityEditor.EditorGUI.EndDisabledGroup();
            UnityEditor.EditorGUILayout.Space();
            base.OnInspectorGUI();
        }
    }
    #endif
}
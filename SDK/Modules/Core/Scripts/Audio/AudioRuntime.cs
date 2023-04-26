using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace OGT
{
    public class AudioRuntime : MonoSingletonSelfGenerated<AudioRuntime>
    {
        [SerializeField] private AudioMixer m_mixer;
        [SerializeField] private SmartAudioSource m_defaultSource;
        
        private List<SmartAudioSource> m_inUseSources = new List<SmartAudioSource>();
        private Queue<SmartAudioSource> m_availableSources = new Queue<SmartAudioSource>();
        
        public List<SmartAudioSource> InUseSources => m_inUseSources;
        public Queue<SmartAudioSource> AvailableSources => m_availableSources;

        public Dictionary<int,Action> PendingAudioCallbacks = new Dictionary<int, Action>();
        
        private const string kMasterLayer = "Master";

        protected override void OnSingletonAwake()
        {
            base.OnSingletonAwake();
            PrePopulatePool();
        }

        private void PrePopulatePool()
        {
            for (int i = 0; i < 10; i++)
            {
                GenerateAndQueueSourceOnLayer(AudioLayer.Default);
            }
        }

        public static int Play(AudioClipDefinition p_clipDefinition, Action p_onComplete = null)
        {
            if (p_clipDefinition == null)
            {
                p_onComplete?.Invoke();
                return 0;
            }
            
            switch (p_clipDefinition.OverridePlayMode)
            {
                default:
                case AudioPlayMode.None:
                    return 0;
                case AudioPlayMode.OneShot:
                    return PlayOnce(p_clipDefinition, p_onComplete);
                case AudioPlayMode.Loop:
                    return PlayLoop(p_clipDefinition, p_onComplete);
            }
        }

        public static bool TryGetClipDefinition(string p_clipName, out AudioClipDefinition p_clipDefinition)
        {
            p_clipDefinition = null;
            return GameResources.Audio.TryGetAudioDefinition(p_clipName, out p_clipDefinition);
        }

        public static int PlayLoop(AudioClipDefinition p_clipDefinition, Action p_onComplete = null)
        {
            if (p_clipDefinition == null || p_clipDefinition.Clip == null)
                return 0;
            
            return Instance.PlayLoop(p_clipDefinition.Clip, p_clipDefinition.Layer, p_onComplete);
        }

        public int PlayLoop(AudioClip p_clip, AudioLayer p_layer, Action p_onComplete = null)
        {
            if (p_clip == null)
                return 0;
            
            var l_source = GetSourceOnLayer(p_layer);
            l_source.PlayLoop(p_clip, p_layer);
            PendingAudioCallbacks.Add(l_source.GetInstanceID(), p_onComplete);
            return l_source.GetInstanceID();
        }

        public static int PlayOnce(AudioClipDefinition p_clipDefinition, Action p_onComplete = null)
        {
            if (p_clipDefinition == null)
                return 0;
            return PlayOnce(p_clipDefinition.Clip, p_clipDefinition.Layer, p_onComplete);
        }

        public static int PlayOnce(AudioClip p_clip, AudioLayer p_layer, Action p_onComplete = null)
        {
            if (p_clip == null)
                return 0;
            
            var l_source = Instance.GetSourceOnLayer(p_layer);
            l_source.PlayOnce(p_clip, p_layer);
            Instance.PendingAudioCallbacks.Add(l_source.GetInstanceID(), p_onComplete);
            return l_source.GetInstanceID();
        }

        public static void Stop(int p_soundID)
        {
            Instance.StopByID(p_soundID);
        }
        
        public void StopByID(int p_soundID)
        {
            SmartAudioSource l_source = InUseSources.FirstOrDefault(p_source => p_source.GetInstanceID() == p_soundID);
            if (l_source == null)
                return;
            
            l_source.Stop();
        }

        public SmartAudioSource GetSourceOnLayer(AudioLayer p_layer)
        {
            if (!m_availableSources.Any())
            {
                GenerateAndQueueSourceOnLayer(p_layer);
            }

            SmartAudioSource recycled = m_availableSources.Dequeue();
            m_inUseSources.Add(recycled);
            
            return recycled;
        }
        
        public SmartAudioSource GenerateAndQueueSourceOnLayer(AudioLayer p_layer)
        {
            SmartAudioSource l_source = Instantiate(m_defaultSource, transform);
            m_availableSources.Enqueue(l_source);
            return l_source;
        }
        
        public static void SetVolume(AudioLayer p_layer, float p_volume)
        {
            Instance.m_mixer.SetVolume(p_layer.ToString(), p_volume);
        }
        
        public static void NotifyAudioFinished(int p_audioId)
        {
            if (!Instance.PendingAudioCallbacks.ContainsKey(p_audioId)) 
                return;
            
            Instance.PendingAudioCallbacks[p_audioId]?.Invoke();
            Instance.PendingAudioCallbacks.Remove(p_audioId);
        }

        public static AudioMixerGroup GetMixerGroupByLayer(AudioLayer p_layer)
        {
            AudioMixerGroup[] l_possible = Instance.m_mixer.FindMatchingGroups($"{kMasterLayer}/{p_layer}");
            if (l_possible.Length == 0)
                return Instance.m_mixer.FindMatchingGroups(kMasterLayer).FirstOrDefault();
            
            return Instance.m_mixer.FindMatchingGroups($"Master/{p_layer}").FirstOrDefault();
        }

        public static void BackToPool(SmartAudioSource p_smartAudioSource)
        {
            Instance.InUseSources.Remove(p_smartAudioSource);
            Instance.AvailableSources.Enqueue(p_smartAudioSource);
        }
    }

    public enum AudioLayer
    {
        Default, UISFX, SoundFX, Music, Ambient, TextToSpeech, Xappy
    }
    
    #if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(AudioRuntime))]
    public class MyMonoBehaviourScriptEditor : UnityEditor.Editor
    {
        AudioRuntime m_target;
        public void OnEnable()
        {
            m_target = target as AudioRuntime;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            m_target = target as AudioRuntime;

            UnityEditor.EditorGUILayout.Space();
            UnityEditor.EditorGUILayout.LabelField("Enqueued Items:");

            foreach (SmartAudioSource source in m_target.AvailableSources)
            {
                UnityEditor.EditorGUILayout.ObjectField(source.gameObject, typeof(GameObject), true);
            }
        }
    }
    #endif
}

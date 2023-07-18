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

        public static int Play(AudioClipDefinition clipDefinition, Action onComplete = null)
        {
            if (clipDefinition == default)
            {
                onComplete?.Invoke();
                return 0;
            }
            
            switch (clipDefinition.OverridePlayMode)
            {
                default:
                case AudioPlayMode.None:
                    return 0;
                case AudioPlayMode.OneShot:
                    return PlayOnce(clipDefinition, onComplete);
                case AudioPlayMode.Loop:
                    return PlayLoop(clipDefinition, onComplete);
            }
        }

        public static bool TryGetClipDefinition(string clipName, out AudioClipDefinition clipDefinition)
        {
            clipDefinition = null;
            return GameResources.Audio.TryGetAudioDefinition(clipName, out clipDefinition);
        }

        public static int PlayLoop(AudioClipDefinition clipDefinition, Action onComplete = default)
        {
            if (clipDefinition == default || clipDefinition.Clip == default)
                return 0;
            
            return Instance.PlayLoop(clipDefinition.Clip, clipDefinition.Layer, onComplete);
        }

        public int PlayLoop(AudioClip clip, AudioLayer layer, Action onComplete = default)
        {
            if (clip == default)
                return 0;
            
            var source = GetSourceOnLayer(layer);
            source.PlayLoop(clip, layer);
            PendingAudioCallbacks.Add(source.GetInstanceID(), onComplete);
            return source.GetInstanceID();
        }

        public static int PlayOnce(AudioClipDefinition clipDefinition, Action onComplete = default)
        {
            if (clipDefinition == default)
                return 0;
            return PlayOnce(clipDefinition.Clip, clipDefinition.Layer, onComplete);
        }

        public static int PlayOnce(AudioClip clip, AudioLayer layer, Action onComplete = default)
        {
            if (clip == default)
                return 0;
            
            var source = Instance.GetSourceOnLayer(layer);
            source.PlayOnce(clip, layer);
            Instance.PendingAudioCallbacks.Add(source.GetInstanceID(), onComplete);
            return source.GetInstanceID();
        }

        public static void Stop(int soundID)
        {
            Instance.StopByID(soundID);
        }
        
        public void StopByID(int soundID)
        {
            SmartAudioSource source = InUseSources.FirstOrDefault(item => item.GetInstanceID() == soundID);
            if (source == null)
                return;
            
            source.Stop();
        }

        public SmartAudioSource GetSourceOnLayer(AudioLayer layer)
        {
            if (!m_availableSources.Any())
            {
                GenerateAndQueueSourceOnLayer(layer);
            }

            SmartAudioSource recycled = m_availableSources.Dequeue();
            m_inUseSources.Add(recycled);
            
            return recycled;
        }
        
        public SmartAudioSource GenerateAndQueueSourceOnLayer(AudioLayer layer)
        {
            SmartAudioSource source = Instantiate(m_defaultSource, transform);
            m_availableSources.Enqueue(source);
            return source;
        }
        
        public static void SetVolume(AudioLayer layer, float volume)
        {
            Instance.m_mixer.SetVolume(layer.ToString(), volume);
        }
        
        public static void NotifyAudioFinished(int audioId)
        {
            if (!Instance.PendingAudioCallbacks.ContainsKey(audioId)) 
                return;
            
            Instance.PendingAudioCallbacks[audioId]?.Invoke();
            Instance.PendingAudioCallbacks.Remove(audioId);
        }

        public static AudioMixerGroup GetMixerGroupByLayer(AudioLayer layer)
        {
            AudioMixerGroup[] possible = Instance.m_mixer.FindMatchingGroups($"{kMasterLayer}/{layer}");
            if (possible.Length == 0)
                return Instance.m_mixer.FindMatchingGroups(kMasterLayer).FirstOrDefault();
            
            return Instance.m_mixer.FindMatchingGroups($"Master/{layer}").FirstOrDefault();
        }

        public static void BackToPool(SmartAudioSource smartAudioSource)
        {
            Instance.InUseSources.Remove(smartAudioSource);
            Instance.AvailableSources.Enqueue(smartAudioSource);
        }
    }

    public enum AudioLayer
    {
        Default, UISFX, SoundFX, Music, Ambient
    }
    
    #if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(AudioRuntime))]
    public class AudioRuntimeEditor : UnityEditor.Editor
    {
        private AudioRuntime m_target;
        
        public void OnEnable()
        {
            m_target = target as AudioRuntime;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            m_target = target as AudioRuntime;
            if (m_target == default)
                return;

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

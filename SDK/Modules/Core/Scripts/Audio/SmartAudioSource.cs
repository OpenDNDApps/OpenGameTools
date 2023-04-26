using System;
using UnityEngine;

namespace OGT
{
    [RequireComponent(typeof(AudioSource))]
    public class SmartAudioSource : MonoBehaviour
    {
        [SerializeField] private AudioLayer m_audioLayer;
        [SerializeField] private AudioSource m_source;
        
        public event Action<int> OnPlayComplete;
        
        public AudioLayer AudioLayer => m_audioLayer;

        public AudioSource Source => m_source;
        
        private bool m_properSetup = false;
        private bool m_checkPlaying = false;

        private void Awake()
        {
            m_source.playOnAwake = false;
            gameObject.name = $"SmartAudioSource_{m_audioLayer}_{GetInstanceID()}";
        }

        public void Setup(AudioClip p_clip, AudioLayer p_layer)
        {
            m_source.clip = p_clip;
            m_audioLayer = p_layer;
            m_source.outputAudioMixerGroup = AudioRuntime.GetMixerGroupByLayer(m_audioLayer);
            m_source.enabled = true;
            m_properSetup = true;
            m_checkPlaying = true;
        }

        public void PlayLoop(AudioClip p_clip, AudioLayer p_layer)
        {
            Setup(p_clip, p_layer);
            if (!IsValidSetup())
                return;

            m_source.loop = true;
            m_source.Play();
        }

        public void PlayOnce(AudioClip p_clip, AudioLayer p_layer)
        {
            Setup(p_clip, p_layer);
            if (!IsValidSetup())
                return;
            
            m_source.loop = false;
            m_source.PlayOneShot(m_source.clip);
        }

        public bool IsValidSetup()
        {
            if (!m_properSetup && (m_source == null || m_source.clip == null || m_source.outputAudioMixerGroup == null))
            {
                Debug.LogError($"This AudioSource failed to setup properly: '{gameObject.name}', Check previous callstack caller.");
                return false;
            }
            return m_properSetup;
        }

        public void BackToPool()
        {
            AudioRuntime.BackToPool(this);
            m_properSetup = false;
            m_source.enabled = false;
            m_checkPlaying = false;
        }

        public void LateUpdate()
        {
            if (!m_checkPlaying) 
                return;

            if (m_source.isPlaying)
                return;
            
            OnFinishPlay();
            
            m_checkPlaying = false;
        }
        
        public void OnFinishPlay()
        {
            OnPlayComplete?.Invoke(GetInstanceID());
            AudioRuntime.NotifyAudioFinished(GetInstanceID());
            if (m_source.clip != null)
            {
                m_source.clip.UnloadAudioData();
            }
            BackToPool();
        }

        public void Stop()
        {
            if (m_source.clip != null)
            {
                m_source.Stop();
            }
            else
            {
                m_checkPlaying = false;
                OnFinishPlay();
            }
        }
    }
}
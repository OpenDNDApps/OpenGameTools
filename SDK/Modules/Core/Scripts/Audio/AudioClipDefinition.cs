using UnityEngine;

namespace OGT
{
    [CreateAssetMenu(fileName = "AudioClipDefinition", menuName = OGTConstants.kGameAudioMenuPath + "AudioClip Definition")]
    public class AudioClipDefinition : BaseScriptableObject
    {
        public AudioClip Clip;
        public AudioLayer Layer;
        public AudioPlayMode OverridePlayMode = AudioPlayMode.None;
        public float VolumeScale = 1f;
    }
    
    public enum AudioPlayMode
    {
        None,
        OneShot,
        Loop
    }
}
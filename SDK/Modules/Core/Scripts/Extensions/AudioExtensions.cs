using UnityEngine;
using UnityEngine.Audio;

public static class AudioExtensions
{
    private const string kVolumeMixerParam = "_Volume";
    
    public static bool SetVolume(this AudioMixer mixer, string name, float value)
    {
        value = Mathf.Clamp01(value);
        value = Mathf.Log10(value) * 20f;
        
        return mixer.SetFloat($"{name}{kVolumeMixerParam}", value);
    }
    
    public static float GetVolume(this AudioMixer mixer, string name)
    {
        float volume = 0f;
        if (!mixer.GetFloat($"{name}{kVolumeMixerParam}", out float value))
            return volume;
        
        volume = Mathf.Exp(value / 20f * Mathf.Log(10f));
        return volume;
    }
}
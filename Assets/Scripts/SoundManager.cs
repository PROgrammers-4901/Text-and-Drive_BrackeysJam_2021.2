using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private List<string> keys = new List<string>();
    public AudioSource PlaySound(AudioClip clip, float volume = 1f)
    {
        GameObject soundObject = new GameObject("Sound");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.volume = volume;
        // TODO: SOUND SETTINGS
        audioSource.Play();

        Destroy(soundObject, audioSource.clip.length);
        
        return audioSource;
    }
}

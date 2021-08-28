using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private List<AudioClip> sounds = new List<AudioClip>();

    public AudioSource PlaySound(string clip, float volume = 1f)
    {
        GameObject soundObject = new GameObject("Sound");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        
        int index = sounds.FindIndex((sound) => sound.name == clip);

        if(index < 0)
            throw new Exception("Could Not Find Clip: " + clip);
        
        audioSource.clip = sounds[index];
        
        PlayClip(audioSource, volume);
        Destroy(soundObject, audioSource.clip.length);
        
        return audioSource;
    }

    private void PlayClip(AudioSource audioSource, float volume)
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.volume = volume;
        // TODO: SOUND SETTINGS
        audioSource.Play();
    }

}

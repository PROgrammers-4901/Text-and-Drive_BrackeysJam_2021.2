using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound {

	public string name;

	public AudioClip clip;

	[Range(0f, 1f)]
	public float volume;
	[Range(0f, 1f)]
	public float volumeVariance;

	[Range(.1f, 3f)]
	public float pitch;
	[Range(0f, 1f)]
	public float pitchVariance;

	public bool loop;

	public AudioMixerGroup mixerGroup;

	//[HideInInspector]
	public AudioSource source;

}

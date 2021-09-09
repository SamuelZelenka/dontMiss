using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioService<T>
{
    public static AudioService<T> instance;

    private Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();
    private AudioSource _audioSource;

    public AudioService(AudioSource audioSource)
    {
        if (instance != null)
        {
            Debug.LogWarning($"{GetType()} was overwritten by new instance");
        }
        _audioSource = audioSource;
        instance = this;
    }

    public void PlayOneShot(string soundKey) => _audioSource.PlayOneShot(clips[soundKey]);
    public void Play(string soundKey)
    {
        _audioSource.clip = clips[soundKey];
        _audioSource.Play();
    }
    public void Play(string soundKey, float delay)
    {
        _audioSource.clip = clips[soundKey];
        _audioSource.PlayDelayed(delay);
    }
    public void Stop() => _audioSource.Stop();
    public void SetPitch(float pitch) => _audioSource.pitch = pitch;
}

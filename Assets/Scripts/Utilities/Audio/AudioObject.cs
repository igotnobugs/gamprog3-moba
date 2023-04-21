using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    private string audioId;
    private AudioSource audioSource;
    private AudioData audioData;

    public string AudioId => audioId;

    public void Initialize(AudioData audioData)
    {
        audioSource = this.gameObject.AddComponent<AudioSource>();

        audioId = audioData.Id;
        audioSource.clip = audioData.audioClip;
        audioSource.volume = audioData.volume / 100f;

        audioSource.Play();
    }

    public void Play()
    {
        if (audioSource == null) return;
        audioSource.Play();
    }

    public void InitializeBGM(AudioData audioData, float volume)
    {
        audioSource = this.gameObject.AddComponent<AudioSource>();

        audioId = audioData.Id;
        audioSource.clip = audioData.audioClip;
        audioSource.volume = volume;
        audioSource.loop = true;

        audioSource.Play();
    }

    public void PlayBGM()
    {
        if (audioSource == null) return;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopPlayingSound()
    {
        if (audioSource == null) return;
        audioSource.Stop();
    }

    public void ChangeVolume(float value)
    {
        if (audioSource == null) return;
        audioSource.volume = value;
    }
}

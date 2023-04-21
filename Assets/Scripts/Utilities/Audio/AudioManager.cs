using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioDatabase audioDatabase;

    [SerializeField] private List<AudioObject> audioList = new List<AudioObject>();

    private bool isPlayingBgm;
    private AudioObject currentTrack;

    public float bgmVolume = 0.8f;

    public void PlayAudio(string audioId)
    {
        if (audioList == null) return;

        if (audioList.Exists(audioObject => audioObject.AudioId == audioId))
        {
            // find the audio data with matching id
            AudioObject audioObject = audioList.Find(audioObject => audioObject.AudioId == audioId);
            audioObject.Play();
        }
        else
        {
            AudioObject audioObject = this.gameObject.AddComponent<AudioObject>();
            audioObject.Initialize(audioDatabase.GetData(audioId));
            audioList.Add(audioObject);
        }
    }

    public void PlayBGM(string audioId)
    {
        if (audioList == null) return;

        if (audioList.Exists(audioObject => audioObject.AudioId == audioId))
        {
            // find the audio data with matching id
            AudioObject audioObject = audioList.Find(audioObject => audioObject.AudioId == audioId);
            if (currentTrack == null)
            {
                currentTrack = audioObject;
            }
            else if (currentTrack != audioObject)
            {
                currentTrack.StopPlayingSound();
                currentTrack = audioObject;
            }
            currentTrack.PlayBGM();
        }
        else
        {
            AudioObject audioObject = this.gameObject.AddComponent<AudioObject>();
            audioObject.InitializeBGM(audioDatabase.GetData(audioId), bgmVolume);
            if (currentTrack == null)
            {
                currentTrack = audioObject;
            }
            else if (currentTrack != audioObject)
            {
                currentTrack.StopPlayingSound();
                currentTrack = audioObject;
            }

            audioList.Add(currentTrack);
        }
    }

    public void ChangeVolume(float value)
    {
        if (audioList == null) return;
        if (currentTrack == null) return;

        bgmVolume = value;

        currentTrack.ChangeVolume(bgmVolume);
    }
}

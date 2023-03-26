using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static bool IsGameMuted;
    public AudioSample[] audioSamples;

    private void Awake()
    {
        IsGameMuted = PlayerPrefs.GetInt("GameVolume") != 1;
        AudioListener.volume = PlayerPrefs.GetInt("GameVolume");
        
        foreach (var audioSample in audioSamples)
        {
            audioSample.source = gameObject.AddComponent<AudioSource>();
            
            audioSample.source.clip = audioSample.clip;
            audioSample.source.volume = audioSample.volume;
            audioSample.source.pitch = audioSample.pitch;
            audioSample.source.loop = audioSample.isLooping;
        }
    }

    private void Update()
    {
        if (GameManager.IsEndPanelActive || GameManager.IsAdsActive) Stop(AudioType.BallRolling);
        if (PlayerTouchController.SwipeDirection != SwipeDirection.Up) return;
        
        Play(AudioType.BallRolling);
    }
    
    public void Play(AudioType audioType)
    {
        var theAudio = Array.Find(audioSamples, audioSample => audioSample.type == audioType);

        if (theAudio == null) return;
        theAudio.source.Stop();
        theAudio.source.Play();
    }

    private void Stop(AudioType audioType)
    {
        var theAudio = Array.Find(audioSamples, audioSample => audioSample.type == audioType);
        theAudio.source.Stop();
    }
}

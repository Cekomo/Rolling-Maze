using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSample[] audioSamples;

    private void Awake()
    {
        // Play(AudioType.GameMusic);
        
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
        if (GameManager.IsEndPanelActive) Stop(AudioType.BallRolling);
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

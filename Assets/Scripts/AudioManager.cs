using System;
using UnityEngine;
using UnityEngine.Audio;
    
public class AudioManager : MonoBehaviour
{
    public AudioSample[] audioSamples;

    private void Awake()
    {
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
        if (PlayerTouchController.SwipeDirection != SwipeDirection.Up) return;
        
        Play(AudioType.BallRolling);
    }
    
    private void Play(AudioType audioType)
    {
        var theAudio = Array.Find(audioSamples, audioSample => audioSample.type == audioType);

        if (theAudio == null) return;
        theAudio.source.Stop();
        theAudio.source.Play();
    }
}

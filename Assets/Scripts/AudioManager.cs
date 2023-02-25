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
            // var audioSource = audioSample.source;
            audioSample.source = gameObject.AddComponent<AudioSource>();
            
            audioSample.source.clip = audioSample.clip;
            audioSample.source.volume = audioSample.volume;
            audioSample.source.pitch = audioSample.pitch;
            audioSample.source.loop = audioSample.isLooping;
        }
    }

    private void Update()
    {
        if (LevelLoader.IsPaused || PlayerTouchController.SwipeDirection is SwipeDirection.Lock) return;
    
        Play("RollingAudio");
    }
    
    private void Play(string audioName)
    {
        var theAudio = Array.Find(audioSamples, audioSample => audioSample.name == audioName);
        if (!theAudio.source.isPlaying) 
            theAudio.source.Play();
    }
}

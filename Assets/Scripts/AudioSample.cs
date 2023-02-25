using UnityEngine;

[System.Serializable]
public class AudioSample
{
    public AudioType type;
    
    public AudioClip clip;

    [Range(0.1f, 2f)] public float volume;
    [Range(0.1f, 2f)] public float pitch;
    public bool isLooping;

    [HideInInspector] public AudioSource source;
}

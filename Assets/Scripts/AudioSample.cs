using UnityEngine;

[System.Serializable]
public class AudioSample
{
    public string name;
    
    public AudioClip clip;

    public float volume;
    public float pitch;
    public bool isLooping;

    [HideInInspector] public AudioSource source;
}

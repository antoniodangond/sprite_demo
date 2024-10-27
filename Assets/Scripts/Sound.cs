using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public string name;
    // Convenience property that checks if this sound is background music,
    // which behaves differently than other audio.
    public bool isBackgroundMusic => name == AudioClipNames.Background;
    public bool isPlaying => source.isPlaying;
    // Set source from AudioManager. Make public for runtime adjustments.
    public AudioSource source = null;
    [Range (0f, 1f)]
    public float volume;

    public void SetSource(AudioSource newSource)
    {
        source = newSource;
        source.clip = clip;
        source.volume = volume;

        if (isBackgroundMusic)
        {
            source.loop = true;
        }
    }

    public void Play(float pitch)
    {
        source.pitch = pitch;
        source.Play();
    }

    public void Pause()
    {
        source.Pause();
    }

    public void UnPause()
    {
        source.UnPause();
    }

    public void SetVolume(float volume)
    {
        source.volume = volume;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Volume to set background music when game is paused
    [Range (0f, 1f)]
    public float pausedBackgroundMusicVolume;
    public Sound[] sounds;

    // TODO: can this be controlled by an audio param?
    // Background music's original volume, which is set when the game is unpaused
    private float backgroundMusicVolume;
    // List of currently paused sounds
    private List<Sound> pausedSounds = new List<Sound>();
    // Map for accessing sounds by name instead of index
    private Dictionary<string, Sound> soundMap = new Dictionary<string, Sound>();

    void Awake()
    {
        foreach (Sound sound in sounds)
        {
            // Add an AudioSource to the sound
            sound.SetSource(gameObject.AddComponent<AudioSource>());
            // Store sound by name
            soundMap[sound.name] = sound;
            // Store background music volume for when the game is unpaused
            if (sound.isBackgroundMusic)
            {
                backgroundMusicVolume = sound.volume;
            }
        }
    }

    private Sound GetSoundByName(string name)
    {
        Sound sound;
        if (soundMap.TryGetValue(name, out sound)) {
            return sound;
        }
        Debug.LogError("Could not find sound by name: " + name);
        return null;
    }

    public void PlaySound(string name, float pitch = 1f)
    {
        Sound sound = GetSoundByName(name);
        if (sound is not null) {
            sound.Play(pitch);
        }
    }

    public bool IsPlaying(string name)
    {
        Sound sound = GetSoundByName(name);
        if (sound is not null) {
            return sound.isPlaying;
        }
        return false;
    }

    // Pause currently playing sounds and update background music volume
    public void EnterPauseState()
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == AudioClipNames.Background)
            {
                sound.SetVolume(pausedBackgroundMusicVolume);
            } else if (sound.isPlaying)
            {
                sound.Pause();
                pausedSounds.Add(sound);
            }
        }
    }

    // Unpause paused sounds and update background music volume
    public void ExitPauseState()
    {
        Sound backgroundMusic = GetSoundByName(AudioClipNames.Background);
        if (backgroundMusic is not null)
        {
            backgroundMusic.SetVolume(backgroundMusicVolume);
        }

        foreach (Sound sound in pausedSounds)
        {
            sound.UnPause();
        }
        // Reset pausedSounds to empty list
        pausedSounds = new List<Sound>();
    }
}

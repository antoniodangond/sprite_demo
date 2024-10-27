using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    // Max pitch variation in either direction
    [Range (0f, 1f)]
    public float pitchVariation;

    private string[] walkingClipNames;
    // Store last played clip index to avoid playing it twice in a row
    private int lastClipIndex;
    private AudioManager audioManager;

    void Awake() {
        // TODO: could probably set the instance in the scene as a public
        // variable on this script
        audioManager = FindObjectOfType<AudioManager>();
        walkingClipNames = new string[]{
            AudioClipNames.Footstep0,
            AudioClipNames.Footstep1,
            AudioClipNames.Footstep2,
            AudioClipNames.Footstep3,
            AudioClipNames.Footstep4,
            AudioClipNames.Footstep5,
        };
        // Initialize as 0 to remove the need for special-case logic
        // when first checking if a clip is currently playing
        lastClipIndex = 0;
    }

    public void PlayWalkingAudio(Vector2 movement)
    {
        // Don't attempt to play walking audio if not moving
        // or if a walking clip is currently playing
        if(
            movement.sqrMagnitude == 0 ||
            audioManager.IsPlaying(walkingClipNames[lastClipIndex])
        )
        {
            return;
        }

        // Prevent playing the same clip twice in a row
        int clipIndex = lastClipIndex;
        while (clipIndex == lastClipIndex)
        {
            clipIndex = Random.Range(0, walkingClipNames.Length);
        }
        // Randomize pitch
        float pitch = Random.Range(1f - pitchVariation, 1f + pitchVariation);
        audioManager.PlaySound(walkingClipNames[clipIndex], pitch);
        // Set new clip index as lastClipIndex
        lastClipIndex = clipIndex;
    }
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    private AudioManager audioManager;

    void Start() {
        audioManager = FindObjectOfType<AudioManager>();
        // Start background music
        audioManager.PlaySound(AudioClipNames.Background);
    }
}

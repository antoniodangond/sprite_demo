using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    public static bool IsGamePaused = false;
    public GameObject pauseMenuUI;

    private AudioManager audioManager;

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused)
            {
                UnPause();
            } else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        IsGamePaused = true;
        // Display pause menu UI
        pauseMenuUI.SetActive(true);
        // Freeze time
        Time.timeScale = 0f;
        // Pause audio
        audioManager.EnterPauseState();
        // Reset selected menu button
        GameObject firstSelected = EventSystem.current.firstSelectedGameObject;
        Button button = firstSelected.GetComponent<Button>();
        // TODO: button animation should restart on select - maybe need animator controller?
        button.Select();
    }

    public void UnPause()
    {
        IsGamePaused = false;
        // Hide pause menu UI
        pauseMenuUI.SetActive(false);
        // Unfreeze time
        Time.timeScale = 1f;
        // Unpause audio
        audioManager.ExitPauseState();
        // Deselect last selected menu button
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void SetSelectedButton(GameObject button)
    {
        EventSystem.current.SetSelectedGameObject(button);
    }
}

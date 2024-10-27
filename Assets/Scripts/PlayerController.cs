using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 movement;
    private PlayerAnimation playerAnimation;
    private PlayerAudio playerAudio;
    private PlayerMovement playerMovement;

    void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerAudio = GetComponent<PlayerAudio>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // Don't do anything if game is paused
        if (PauseController.IsGamePaused)
        {
            return;
        }
        // Handle input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        // Set animation
        playerAnimation.SetAnimationParams(movement);
        // Set audio
        playerAudio.PlayWalkingAudio(movement);
    }

    void FixedUpdate()
    {
        // Move player rigidbody during Fixedupdate to cooperate with physics
        playerMovement.Move(movement);
    }
}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;

    public void Move(Vector2 movement)
    {
        // Normalize movement vector to set mangitutude to 1. This prevents speed
        // increase when moving diagonally. Set velocity to movement vector,
        // so that physics are respected.
        rb.velocity = movement.normalized * moveSpeed;
    }
}

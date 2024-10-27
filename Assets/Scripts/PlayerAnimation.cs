using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;

    public void SetAnimationParams(Vector2 movement)
    {
        // Use movement.sqrMagnitutde as a proxy for "is moving"
        animator.SetFloat(AnimatorParams.Speed, movement.sqrMagnitude);
        // Only set direction when moving, so that idle animation
        // will play in the last moved direction
        if (movement.sqrMagnitude > 0)
        {
            // If moving horizontal or diagonal, use horizontal animations.
            // Set opposite axis to 0 to avoid inconsistent behavior when
            // both axes have a non-zero value.
            if (Math.Abs(movement.x) > 0)
            {
                animator.SetFloat(AnimatorParams.Horizontal, movement.x);
                animator.SetFloat(AnimatorParams.Vertical, 0f);
            }
            else
            {
                animator.SetFloat(AnimatorParams.Horizontal, 0f);
                animator.SetFloat(AnimatorParams.Vertical, movement.y);
            }
        }
    }
}

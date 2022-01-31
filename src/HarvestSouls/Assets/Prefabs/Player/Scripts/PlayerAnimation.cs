using UnityEngine;

public class PlayerAnimator
{
    private readonly Animator animator;

    //Animation String IDs
    private int horizontal;
    private int vertical;
    private int speed;

    public PlayerAnimator(Animator playerAnimator)
    {
        animator = playerAnimator;
        Initalize();
    }

    private void Initalize()
    {
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
        speed = Animator.StringToHash("Speed");
    }

    public void AnimateMovement(Vector2 movement)
    {
        animator.SetFloat(horizontal, movement.x);
        animator.SetFloat(vertical, movement.y);
        animator.SetFloat(speed, movement.sqrMagnitude);
    }
}
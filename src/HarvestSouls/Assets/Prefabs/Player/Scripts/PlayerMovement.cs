using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public float SprintSpeed = 10f;

    public Rigidbody2D RigidBody;
    public Animator Animator;

    private Vector2 movement;
    private PlayerAnimator playerAnimator;
    private InputMaster input;
    private bool isSprinting;

    void Awake()
    {
        playerAnimator = new PlayerAnimator(Animator);
        input = new InputMaster();
        input.Player.Enable();   
    }

    void Update()
    {
        HandlePlayerInput();
        HandleAnimation();
    }

    private void HandleAnimation()
    {
        playerAnimator.AnimateMovement(movement);
    }

    private void HandlePlayerInput()
    {
        movement = input.Player.Movement.ReadValue<Vector2>();
        isSprinting = input.Player.Sprint.IsPressed();
    }

    void FixedUpdate()
    {
        var speed = isSprinting ? SprintSpeed : MoveSpeed;
        RigidBody.MovePosition(RigidBody.position + movement * speed * Time.fixedDeltaTime);
    }
}
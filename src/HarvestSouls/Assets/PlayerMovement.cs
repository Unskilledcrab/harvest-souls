using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 movement;
    private InputMaster controls;

    void Awake()
    {
        controls = new InputMaster();
        controls.Player.Enable();
        //controls.Player.Sprint.performed += x => Sprint();
    }

    void Update()
    {
        movement = controls.Player.Movement.ReadValue<Vector2>();

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
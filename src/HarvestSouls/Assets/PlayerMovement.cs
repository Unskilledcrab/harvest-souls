using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Input;

public class PlayerMovement : MonoBehaviour
{

    public InputMaster controls;

    void Awake()
    {
        controls.Player.Sprint.performed += x => Sprint();
        controls.Player.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    void Move(Vector2 direction)
    {
        
    }

    void Sprint()
    {
        
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}

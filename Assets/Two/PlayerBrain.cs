using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBrain : MonoBehaviour
{
    [SerializeField] InputActionReference _moveInput; //Input

    [SerializeField] EntityMovement _movement; //Actions

    private void Start()
    {
        _moveInput.action.started += Move;

        _moveInput.action.performed += Move;

        _moveInput.action.canceled += End;
    }

    private void Move(InputAction.CallbackContext obj)
    {
        Vector2 dir = obj.ReadValue<Vector2>();
        _movement.Direction = new Vector3(dir.x, 0, dir.y);
    }

    private void End(InputAction.CallbackContext obj)
    {

        _movement.Direction = Vector2.zero;
    }
}

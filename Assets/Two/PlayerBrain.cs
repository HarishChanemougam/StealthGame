using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBrain : MonoBehaviour
{
    [SerializeField] InputActionReference _moveInput; //Input
    [SerializeField] InputActionReference _runInput; //Input

    [SerializeField] EntityMovement _movement; //Actions

    private void Start()
    {
        _moveInput.action.started += Move;

        _moveInput.action.performed += Move;

        _moveInput.action.canceled += End;

        _runInput.action.started += StartRun;
        _runInput.action.canceled += StopRun;

    }

    private void OnDestroy()
    {
        _moveInput.action.started -= Move;

        _moveInput.action.performed -= Move;

        _moveInput.action.canceled -= End;

        _runInput.action.started -= StartRun;
        _runInput.action.canceled -= StopRun;
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
    private void StartRun(InputAction.CallbackContext obj)
    {
        _movement.IsRunning = true;
    }
    private void StopRun(InputAction.CallbackContext obj)
    {
        _movement.IsRunning = false;
    }

}

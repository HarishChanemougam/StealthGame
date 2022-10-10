using Polytope;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [SerializeField] Camera _playerCamera;
    [SerializeField] CharacterController _characterController;
    [SerializeField] float _speed;
    [SerializeField] float _runSpeed;

    Vector3 _direction;
     bool _isRunning;

    public Vector3 Direction
    {
        get => _direction;
        set => _direction = value;
    }

    public bool IsRunning
    {
        get => _isRunning;
        set => _isRunning = value;
    }

    private void Update()
    {
        Vector3 CalculatedDirection = _direction * Time.deltaTime * _speed; //Calculating the direction that wa want to move
       
        if (_isRunning)
        {
        CalculatedDirection *= _runSpeed;
        }
       
       
        CalculatedDirection = _playerCamera.transform.TransformDirection(CalculatedDirection); //Demanding the camera fro his direction
        CalculatedDirection.y = 0;

        _characterController.Move(CalculatedDirection); //CharacterController Moving calculation

        float YAxis = _playerCamera.transform.rotation.y;
        _characterController.transform.rotation = Quaternion.Euler(0, YAxis, 0);
    }
}

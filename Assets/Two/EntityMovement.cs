using Polytope;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [SerializeField] Camera _playerCamera;
    [SerializeField] CharacterController _characterController;
    [SerializeField] float _speed;

    Vector3 _direction;

    public Vector3 Direction
    {
        get => _direction;
        set => _direction = value;
    }

    private void Update()
    {
        Vector3 CalculatedDirection = _direction * Time.deltaTime * _speed; //Calculating the direction that wa want to move

        CalculatedDirection = _playerCamera.transform.TransformDirection(CalculatedDirection); //Demanding the camera fro his direction
        CalculatedDirection.y = 0;

        _characterController.Move(CalculatedDirection); //CharacterController Moving calculation

        float YAxis = _playerCamera.transform.rotation.y;
        _characterController.transform.rotation = Quaternion.Euler(0, YAxis, 0);
    }
}

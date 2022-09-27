using Retro.ThirdPersonCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] PlayerMovement _player;
    private Rigidbody _rb;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _player = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        transform.LookAt(_player.transform.position);
    }

    private void FixedUpdate()
    {
        /*_rb Vector3 = (new Vector3 = transform.forward * _moveSpeed * 3);*/
    }
}

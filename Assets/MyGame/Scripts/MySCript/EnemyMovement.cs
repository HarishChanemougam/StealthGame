using NaughtyAttributes;
using Retro.ThirdPersonCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _attackDistance;
    [SerializeField] Animator _animator;
    [SerializeField] PlayerMovement _player;
    [SerializeField] Rigidbody _root;
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

using NaughtyAttributes;
using Retro.ThirdPersonCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Rendering.UI;
using Unity.VisualScripting;
using UnityEngine.Rendering;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] float _moveSpeed; //Enemy Moving Speed
    [SerializeField] float _attackDistance; //Attacking Zone
    [SerializeField] Animator _animator; //Enemy Animator
    [SerializeField] PlayerMovement _player; //Player Movement Script To Enemy
  

   
    public bool IsWalking => _appliedVector.magnitude > 0.01f;

    public float WalkDistance => _appliedVector.magnitude;

    Vector3 _directionAsked;
    Vector3 _appliedVector;
    Vector3 _direction;
    string _enemy;
    bool idle_normal;
    bool idle_combat;
    bool dead;
    bool damage_001;
    bool attack_short_001;
    bool move_forward;
    bool move_forward_fast;

    PlayerTag _target;
    bool _attack = true;
    private object _health;

    IEnumerator AttackRoutine()
    {
        _attack = false;
        yield return new WaitForSeconds(1f);
        _attack = true;
    }


   /* private void Start()
    {
       _enemy = GetComponent<Rigidbody>();
        _player = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        transform.LookAt(_player.transform.position);

        if (_health.CurrentHealth <= 0)
        {
            _animator.SetBool("dead", true);
        }

        else
        {
            _animator.SetBool("dead", false);
        }
    }

    public void SetTarget(PlayerTag player)
    {
        _target = player;
    }
    internal void ClearTarget()
    {
        _target = null;
    }

    private void FixedUpdate()
    {
        *//*_rb Vector3 = (new Vector3 = transform.forward * _moveSpeed * 3);*//*

        _animator.SetBool("move_forward", _direction.magnitude > 0.1f);
        _animator.SetBool("idle_normal", idle_normal);
        _animator.SetBool("idle_combat", idle_combat);
        _animator.SetBool("damage_001", damage_001);

        Vector3 direction = Vector3.zero;
        float distanceToPlayer = 10000f;

        if (_target != null)
        {
            distanceToPlayer = Vector3.Distance(_target.transform.position, transform.position);

            direction = _target.transform.position - transform.position;
            direction.Normalize();

        }
        _appliedVector = direction * Time.deltaTime * _moveSpeed;

        if (distanceToPlayer < _attackDistance && _attack)
        {
            _animator.SetTrigger("attack_short_001");
            _player.AttackAllCharacters();
            StartCoroutine(AttackRoutine);

            else
            {
                _root.MovePosition(_root.position + _appliedVector);
            }
        }

    }*/

}

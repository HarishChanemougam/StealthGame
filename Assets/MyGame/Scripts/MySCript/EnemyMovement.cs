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
using System.Transactions;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] float _moveSpeed; //Enemy Moving Speed
    [SerializeField] float _attackDistance; //Attacking Zone
    [SerializeField] Animator _animator; //Enemy Animator
    [SerializeField] PlayerMovement _player; //Player Movement Script To Enemy
    [SerializeField] CharacterController _characterController; //Enemy Character Controller
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] Transform _playerPos;

    [SerializeField] PlayerHealth _PlayerHealth;
    Vector3 _Vector; //Vector For Movements
    Vector3 _direction; //The Direction That The Enemy Needs To Go
    CharacterController _enemy; //The Enemy
    bool idle_normal; //Enemy Normal Pose Animation
    bool idle_combat; //Enemy Combat Pose Animation
    bool dead; //Enemy Death Animation
    bool damage_001; //Enemy Damage Animation
    bool attack_short_001;//Enemy Attack Animation
    bool move_forward => _Vector.magnitude > 0.01f; //Enemy Moving Animation With Rotation
    float move_forward_fast => _Vector.magnitude; // Enemy Sprint Animation With Rotation


    PlayerTag _target; //player Tag For Enemy To Find
    bool _attack; //Enemy Attack Bool
    EnemyHealth _health; //Enmey Health Loacation
    Coroutine _attackRoutine;

    IEnumerator AttackRoutine() // Enemy Waiting Time
    {
        _PlayerHealth.Damage();
        _attack = false; // If Attack Input Is Not On
        yield return new WaitForSeconds(1f); //Waiting Time
        _attack = true; //If Attack Input Is On
        _attackRoutine = null;
       

    }


    private void Start()
    {
        _enemy = GetComponent<CharacterController>();// Character Controller For The Enemy
        _direction = transform.forward * _moveSpeed * 3; //Make Enemy Move
        _characterController.transform.rotation = Quaternion.Euler(0, _player.transform.rotation.eulerAngles.y, 0); //Rotation Mecanisme For Enemy
        _player = FindObjectOfType<PlayerMovement>(); //Chasing The Player 
    }

    /*private void Update()
    {
        _agent.SetDestination(_playerPos.position);
    }*/

    public void SetTarget(PlayerTag player) //Setting The Player As The Target For The Player
    {
        _target = player; // Target Is The Player
    }
    internal void ClearTarget() //Radius To Find The Target
    {
        _target = null; //If Can't Find The Player In His Radius, The Target Is None Then
    }

    private void FixedUpdate()
    {
        _animator.SetBool("move_forward", _direction.magnitude > 0.1f); //Enemy Moving Animator
        _animator.SetBool("idle_normal", idle_normal); //Enemy Normal Idle Animator
        _animator.SetBool("idle_combat", idle_combat); //Enemy Combat idle Animator
        _animator.SetBool("damage_001", damage_001); //Enemy Damage Animator
        

        Vector3 direction = Vector3.zero; //End Movements
        float distanceToPlayer = 1f; //Recahable Radius To The Player 

        if (_target != null) //Info To Do If No Target 
        {
            distanceToPlayer = Vector3.Distance(_target.transform.position, transform.position); //Getting To The Player

            direction = _target.transform.position - transform.position; //Moving Towards The Player Calculation
            direction.Normalize(); //Pointing The Direction 
        }
        _Vector = direction * Time.deltaTime * _moveSpeed; //Moving Speed Caluculation

        if (_target != null && distanceToPlayer < _attackDistance && _attackRoutine==null) //Enemy Attack Distance
        {
            _animator.SetTrigger("attack_short_001"); //Attack Animator

            _player.AttackAllCharacters(); //Attack Characters      

            _attackRoutine = StartCoroutine(AttackRoutine()); //Waiting Time
        }
        else//Root Motion To Move
        {
            _enemy.Move(_Vector); 
        }

    }

}

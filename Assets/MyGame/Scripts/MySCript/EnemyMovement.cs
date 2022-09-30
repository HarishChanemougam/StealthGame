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
    [SerializeField] CharacterController _characterController; //Enemy Character Controller
  


    
    Vector3 _Vector; //Vector For Rotation
    Vector3 _direction; //The Direction That The Enemy Needs To Go
    string _enemy; //The Enemy
    bool idle_normal; //Enemy Normal Pose Animation
    bool idle_combat; //Enemy Combat Pose Animation
    bool dead; //Enemy Death Animation
    bool damage_001; //Enemy Damage Animation
    bool attack_short_001;//Enemy Attack Animation
    bool move_forward => _Vector.magnitude > 0.01f; //Enemy Moving Animation With Rotation
    float move_forward_fast => _Vector.magnitude; // Enemy Sprint Animation With Rotation


    PlayerTag _target; //player Tag For Enemy To Find
    bool _attack; //Enemy Attack Bool
    private object _health; //Enmey Health Loacation

    IEnumerator AttackRoutine() // Enemy Waiting Time
    {
        _attack = false; // If Attack Input Is Not On
        yield return new WaitForSeconds(1f); //Waiting Time
        _attack = true; //If Attack Input Is On
    }


    private void Start()
    {
        _enemy = GetComponent<CharacterController>();// Character Controller For The Enemy

        CharacterController Vector3 = (new Vector3 = transform.forward * _moveSpeed * 3); //Make Enemy Move

        _characterController.transform.rotation = Quaternion.Euler(0, _player.transform.rotation.eulerAngles.y, 0); //Rotation Mecanisme For Enemy

        _player = FindObjectOfType<PlayerMovement>(); //Chasing The Player 
    }

    private void Update()
    {
        transform.LookAt(_player.transform.position); //Enemy Looking At The Player 

        if (_health.CurrentHealth <= 0) //If The Enemy Health Is Under Zero He Dies
        {
            _animator.SetBool("dead", true); //Animation Set For Enemy's Death
        }

        else
        {
            _animator.SetBool("dead", false); //Animation Will Not Work If The Enmey Is Alive
        }
    }

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
        float distanceToPlayer = 10000f; //Recahable Radius To The Player 

        if (_target != null) //Info To Do If No Target 
        {
            distanceToPlayer = Vector3.Distance(_target.transform.position, transform.position); //Getting To The Player

            direction = _target.transform.position - transform.position; //Moving Towards The Player Calculation
            direction.Normalize(); //Pointing The Direction 

        }
        _Vector = direction * Time.deltaTime * _moveSpeed; //Moving Speed Caluculation

        if (distanceToPlayer < _attackDistance && _attack) //Enemy Attack Distance
        {
            _animator.SetTrigger("attack_short_001"); //Attack Animator

            _player.AttackAllCharacters(); //Attack Characters

            StartCoroutine(AttackRoutine); //Waiting Time

            else
            {
                _root.MovePosition(_root.position + _Vector); 
            }
        }

    }

}

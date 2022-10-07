using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.InputSystem.InputSettings;
using static UnityEngine.LightAnchor;

namespace Retro.ThirdPersonCharacter
{
    public class PlayerMovement : MonoBehaviour
    {
        //Player Movement//
    [SerializeField] InputActionReference _moveInput; //PlayerMoveInput
    [SerializeField] InputActionReference _jumpInput; //PlayerJumpInput
    [SerializeField] InputActionReference _attackInput; //PlayerAttaclInput
    [SerializeField] Transform _root; //RootMotion
    [SerializeField] Animator _animator; //PlayerAnimator
    [SerializeField] float _movingThreshold; //Threshold
    [SerializeField] float _speed; //PlayerSpeed


        //Player Jump//
        [SerializeField] float _gravity = 9f; //WorldSpaceGravity
        [SerializeField] float _jumpForce;
        [SerializeField] float _pullDown = 3f; //GravityTowardsGround


        #region
        //Camera Fallow//
    [SerializeField] bool _followCameraOrientation;//ScriptKevinCamera //FollowCamera
    [SerializeField, ShowIf(nameof(_followCameraOrientation))] Camera _camera;//ScriptKevinCamera  //CameraOrientation
    [SerializeField] CharacterController _controller;//ScriptKevinCamera //CharacterController
        #endregion

        private PlayerInput playerInput; //PlayerInputForCamera
        


        EnemyHealth _enemyHealth;
        public EnemyHealth EnemyTarget
        {
            get => _enemyHealth;
            set => _enemyHealth = value;
        }

        /*Vector3 _playerMovement;*/
        bool _jump; //PlayerJumpingHeight
        bool _attack;
       
        Vector3 _direction; //PLayerMovingDirection
        Vector3 _aimDirection; //PlayerAimingDirection


        #region
        Vector3 _playerMovement;//ScriptKevinCamera
        Vector3 _calculatedDirection;//ScriptKevinCamera

        public event UnityAction<Vector3> OnMove;//PhysicalImpactOnToMove
        #endregion
        #region
        /*[SerializeField] Animator _animator;
        [SerializeField] PlayerInput _playerInput;
        private Combat _combat;
        private CharacterController _characterController;

        private Vector2 lastMovementInput;
        private Vector3 moveDirection = Vector3.zero;

        [SerializeField] Rigidbody _rb;
        [SerializeField] float speed = 5;
        [SerializeField] float jumpSpeed = 5;

        [SerializeField] float MaxSpeed = 10;
        private float DecelerationOnStop = 0.00f;

        Vector3 _direction;
        private float z;*/

        /*private void Start()
        {
            _animator = GetComponent<Animator>();
            _playerInput = GetComponent<PlayerInput>();
            _combat = GetComponent<Combat>();
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_animator == null) return;

            if (_combat.AttackInProgress)
            {
                StopMovementOnAttack();
            }
            else
            {
                Move();
            }

        }
        private void Move()
        {
            var x = _playerInput.MovementInput.x;
            var y = _playerInput.MovementInput.y;

            bool grounded = _rb;

            if (grounded)
            {
                moveDirection = new Vector3(x, 0, z);
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= MaxSpeed;

                if (_playerInput.JumpInput)
                    moveDirection.x = jumpSpeed;
            }

            _rb.MovePosition(_rb.transform.position + (_direction * Time.fixedDeltaTime * speed * 5));
            _rb.MovePosition(moveDirection * Time.deltaTime);

            _animator.SetFloat("InputX", x);
            _animator.SetFloat("InputY", y);
            _animator.SetBool("IsInAir", !grounded);
        }

        private void StopMovementOnAttack()
        {
            var temp = lastMovementInput;
            temp.x -= DecelerationOnStop;
            temp.y -= DecelerationOnStop;
            lastMovementInput = temp;

            _animator.SetFloat("InputX", lastMovementInput.x);
            _animator.SetFloat("InputY", lastMovementInput.y);
        }*/
        #endregion


        //////////////////////////PLayer Movement Joystick///////////////////////////
        private void Start()
        {
            _moveInput.action.started += StartMove; //Player Starting An Action (walk)
            _moveInput.action.performed += StartMove; //Player Perfoming A Move (walk)
            _moveInput.action.canceled += EndMove; //Player Ending An Action ( walk)


            _jumpInput.action.started += StartJump;

            _attackInput.action.started += StartAttack; //Player Starting to Attack 


        }

        private void StartJump(InputAction.CallbackContext obj)
        {
            _jump = true;
        }

        private void Update()
        {
            Debug.Log($"{_playerMovement}");

            if (_followCameraOrientation)   // Follow camera orientation //ScriptKevinCamera
            {
                _controller.transform.rotation = Quaternion.Euler(0, _camera.transform.rotation.eulerAngles.y, 0);
            }

            if (_playerMovement.magnitude > _movingThreshold)
            {
               _animator.SetBool("IsMoving", true); //Moving Animator
               _animator.SetFloat("InputX", _playerMovement.x); //X Axis Control
               _animator.SetFloat("InputZ",_playerMovement.z); //Y Axis Control
                
                var tmpDirection = (_playerMovement * _speed * Time.deltaTime);//ScriptKevinCamera
                var forwardForCamera = _camera.transform.TransformDirection(tmpDirection);//ScriptKevinCamera
                _calculatedDirection.x = forwardForCamera.x;//ScriptKevinCamera
                _calculatedDirection.z = forwardForCamera.z;//ScriptKevinCamera
            }
            else
            {
                _animator.SetBool("IsMoving", false); //Animator Stops If No Input Action
                _calculatedDirection.x = 0;
                _calculatedDirection.z = 0;
                _animator.SetFloat("InputX", 0);
                _animator.SetFloat("InputZ", 0);
            }

            if(_controller.isGrounded) // To Know If The Player Touching The Ground Or Not 
            {
                _calculatedDirection.y = 0;
            }
            else
            {
                _calculatedDirection.y += _gravity * Time.deltaTime;
            }

            if(_jump) //Euuuu....Jump....Like The Thing We Do In A Trampoline 
            {
                _jump = false;
                if(_controller.isGrounded)
                {
                    _calculatedDirection.y = _jumpForce; //SCriptKevin //To Jump
                }
            }

            _controller.Move(_calculatedDirection * Time.deltaTime); //Camera Attached To Player
            OnMove?.Invoke(_calculatedDirection); //Camera Attached To Player
        }

        private void StartMove(InputAction.CallbackContext obj) //Staring A Move
        {
            var joystick = obj.ReadValue<Vector2>();
            _playerMovement = new Vector3(joystick.x, 0, joystick.y);
        }

        private void EndMove(InputAction.CallbackContext obj) //Ending A Move
        {
            
            _playerMovement = new Vector3( 0, 0, 0);
        }

        private void StartAttack(InputAction.CallbackContext obj) //Starting to Attack
        {
            _animator.SetTrigger("Attack");

            _enemyHealth.Damage();
            _attack = false;
            _attack = true;

        }

        private void AttackAllCharacters (InputAction.CallbackContext obj)
        {
            _animator.SetTrigger("Attack");
        }

        internal void AttackAllCharacters()
        {
            //throw new NotImplementedException();
        }
    }
}
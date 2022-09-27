using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.InputSystem.InputSettings;
using static UnityEngine.LightAnchor;

namespace Retro.ThirdPersonCharacter
{
    [RequireComponent(typeof(PlayerInput))]
    /*[RequireComponent(typeof(Animator))]*/
  /*  [RequireComponent(typeof(Combat))]
    [RequireComponent(typeof(Rigidbody))]*/
    public class PlayerMovement : MonoBehaviour
    {

    [SerializeField] InputActionReference _moveInput;
    [SerializeField] Transform _root;
    [SerializeField] Animator _animator;
    [SerializeField] float _movingThreshold;
    [SerializeField] float _speed;

        #region
    [SerializeField] bool _followCameraOrientation;//ScriptKevinCamera
    [SerializeField, ShowIf(nameof(_followCameraOrientation))] Camera _camera;//ScriptKevinCamera
    [SerializeField] CharacterController _controller;//ScriptKevinCamera
        #endregion

        private PlayerInput playerInput;
        string _playet;
        float _speedOfMovementVariabale;
        Vector2 _playerMovement;
        Vector2 _direction;
        Vector3 _aimDirection;

        #region
        Vector3 _directionFromBrain;//ScriptKevinCamera
        Vector3 _calculatedDirection;//ScriptKevinCamera
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
        private void Start()
        {
            _moveInput.action.started += StartMove;
            _moveInput.action.performed += UpdateMove;
            _moveInput.action.canceled += EndMove;        
        }

        private void Update()
        {
            _root.transform.Translate(_playerMovement * Time.deltaTime * _speed * 5);

            if(_playerMovement.magnitude > _movingThreshold)
            {
                if (_animator == null) return;

                _animator.SetBool("IsMoving", true);
               _animator.SetFloat("InputX", _playerMovement.x);
               _animator.SetFloat("InputY", _playerMovement.y);

                #region
                var tmpDirection = (_directionFromBrain * _speed * Time.deltaTime);//ScriptKevinCamera
                var forwardForCamera = _camera.transform.TransformDirection(tmpDirection);//ScriptKevinCamera
                _calculatedDirection.x = forwardForCamera.x;//ScriptKevinCamera
                _calculatedDirection.z = forwardForCamera.z;//ScriptKevinCamera

                if (_followCameraOrientation)   // Follow camera orientation //ScriptKevinCamera
                {
                    var lookAtDirection = new Vector3(_camera.transform.forward.x, 0, _camera.transform.forward.z);//ScriptKevinCamera
                    _controller.transform.LookAt(_controller.transform.position + lookAtDirection);//ScriptKevinCamera
                }
                else  // Follow direction applied //ScriptKevinCamera
                {

                }
                #endregion
            }

            else
            {
                _animator.SetBool("IsMoving", false);
           }
        }

        private void StartMove(InputAction.CallbackContext obj)
        {
            _playerMovement = obj.ReadValue<Vector2>();
        }

        private void UpdateMove(InputAction.CallbackContext obj)
        {
            _playerMovement = obj.ReadValue<Vector2>();
        }

        private void EndMove(InputAction.CallbackContext obj)
        {
            _playerMovement = new Vector3(0, 0, 0);
        }

        private void LateUpdate()
        {
            playerInput = GetComponent<PlayerInput>();
        }
     
    }

    
}
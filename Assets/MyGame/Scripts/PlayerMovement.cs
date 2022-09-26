using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Retro.ThirdPersonCharacter
{
    [RequireComponent(typeof(PlayerInput))]
    /*  [RequireComponent(typeof(Animator))]
      [RequireComponent(typeof(Combat))]*/
   /* [RequireComponent(typeof(Rigidbody))]*/
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        [SerializeField] PlayerInput _playerInput;
        private Combat _combat;
       /* private CharacterController _characterController;*/

        private Vector2 lastMovementInput;
        private Vector3 moveDirection = Vector3.zero;

        [SerializeField] Rigidbody _rb;
        [SerializeField] float speed = 5;
        [SerializeField] float jumpSpeed = 5; 

        [SerializeField] float MaxSpeed = 10;
        private float DecelerationOnStop = 0.00f;

        Vector3 _direction;
        private float z;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _playerInput = GetComponent<PlayerInput>();
            _combat = GetComponent<Combat>();
            /*_rb = GetComponent<Rigidbody>();*/
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
            /*_animator.SetBool("IsInAir", !grounded);*/
        }

        private void StopMovementOnAttack()
        {
            var temp = lastMovementInput;
            temp.x -= DecelerationOnStop;
            temp.y -= DecelerationOnStop;
            lastMovementInput = temp;

            _animator.SetFloat("InputX", lastMovementInput.x);
            _animator.SetFloat("InputY", lastMovementInput.y);
        }
    }
}
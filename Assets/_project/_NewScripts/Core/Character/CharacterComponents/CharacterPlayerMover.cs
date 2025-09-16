using UnityEngine;
using UnityEngine.InputSystem;

namespace BoatGame.Core.Character.Components
{
    public class CharacterPlayerMover : IMover
    {
        private Transform _transform;
        private CharacterController _controller;

        private float _speedOfMoving;

        private float _verticalVelocity;
        private float _gravityForce;

        private Vector2 currentDirection;

        public CharacterPlayerMover(Transform transform,CharacterController controller, float moveSpeed = 5f, float gravityForce = 9.81f)
        {
            _transform = transform;
            _controller = controller;
            _speedOfMoving = moveSpeed;
            _gravityForce = gravityForce;
        }

        public void Update(float deltaTime)
        {
            if (!_controller.isGrounded)
            {
                _verticalVelocity += _gravityForce * deltaTime;
            }
            else _verticalVelocity = 0;

            Vector3 characterMove = _transform.TransformDirection(
                new Vector3(currentDirection.x, 0, currentDirection.y)) * _speedOfMoving * deltaTime;

            Vector3 totalMove = characterMove;

            float clampedY = Mathf.Clamp(_verticalVelocity, -20f, 20f);
            
            totalMove.y -= clampedY * deltaTime;

            _controller.Move(totalMove);
        }

        public void SetDirection(Vector2 direction)
        {
            currentDirection = direction.normalized;
        }
    }
}
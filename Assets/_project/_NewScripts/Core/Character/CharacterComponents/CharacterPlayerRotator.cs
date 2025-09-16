using Unity.Cinemachine;
using UnityEngine;

namespace BoatGame.Core.Character.Components
{
    public class CharacterPlayerRotator : IRotator
    {
        private Transform _transform;
        private CinemachineCamera _camera;

        private float mouseX;
        private float mouseY;
        public float Sensitivity;

        private Vector2 _currentDirection;

        public CharacterPlayerRotator(Transform transform, CinemachineCamera camera, float sens = 90f)
        {
            _transform = transform;
            _camera = camera;
            Sensitivity = sens;
        }

        public void Update(float deltaTime)
        {
            mouseX += _currentDirection.x * deltaTime * Sensitivity;
            mouseY -= _currentDirection.y * deltaTime * Sensitivity;
            mouseY = Mathf.Clamp(mouseY, -75, 75);

            _camera.gameObject.transform.localEulerAngles = new Vector3(mouseY, 0f, 0f);
            _transform.localEulerAngles = new Vector3(0f, mouseX, 0f);
        }
        public void SetDirection(Vector2 direction)
        {
            _currentDirection = direction.normalized;
        }
    }
}
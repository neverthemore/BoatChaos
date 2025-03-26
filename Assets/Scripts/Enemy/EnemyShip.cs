using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothTime = 2f; // Время для выравнивания
    [SerializeField] private float _rotationSpeed = 5f; // Скорость поворота

    private Vector3 _velocity = Vector3.zero;

    private void Update()
    {
        // Получаем текущие позицию и поворот основного корабля
        Vector3 targetPosition = Ship.LastShipPosition + Ship.LastShipRotation * _offset;
        Quaternion targetRotation = Ship.LastShipRotation;

        // Плавное перемещение к цели
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref _velocity,
            _smoothTime
        );

        // Плавный поворот в сторону основного корабля
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            _rotationSpeed * Time.deltaTime
        );


    }

    public void SetOffset(Vector3 offset)
    {
        _offset = offset;
    }
}

using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothTime = 2f; // ����� ��� ������������
    [SerializeField] private float _rotationSpeed = 5f; // �������� ��������

    private Vector3 _velocity = Vector3.zero;

    private void Update()
    {
        // �������� ������� ������� � ������� ��������� �������
        Vector3 targetPosition = Ship.LastShipPosition + Ship.LastShipRotation * _offset;
        Quaternion targetRotation = Ship.LastShipRotation;

        // ������� ����������� � ����
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref _velocity,
            _smoothTime
        );

        // ������� ������� � ������� ��������� �������
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

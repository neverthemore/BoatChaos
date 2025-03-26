using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    private Vector3 _offset;
    [SerializeField] private float _smoothTime = 1f; // ����� ��� ������������
    [SerializeField] Cannon _cannon;

    [SerializeField] private float _attackCooldown;
    private float _currentCooldown = 0;

    private Vector3 _velocity = Vector3.zero;

    private bool _needToFloat = false;


    private void Update()
    {
        FloatToPoint();

        if (!_needToFloat && _currentCooldown <= 0)
        {
            Attack();
            _currentCooldown = _attackCooldown;
        }
        else if (_currentCooldown >= 0) _currentCooldown -= Time.deltaTime; 

    }

    public void SetOffset(Vector3 point)  //���� ����� �������
    {
        _offset = point;
        _needToFloat = true;
    }

    //���� ������ ���������� �� �������� � ��������� ����

    private void FloatToPoint() //������ � ����� ���� ����
    {
        if (_needToFloat)
        {
            Vector3 targetPosition = _offset;

            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPosition,
                ref _velocity,
                _smoothTime
            );
           
            if (Vector3.Distance(transform.position, _offset) < 5f) _needToFloat = false;
        }
    }

    private void Attack()
    {
        //����� ����� ������� � ��������� Fire();
        if (_cannon != null)
        {
            _cannon.Fire();
        }
    }

    public void SinkTheShip()
    {
        //����� ������� + ������
        Debug.Log("��������� ������� �������");
        _offset = new Vector3(50, -30, 0);
        _needToFloat = true;

        Destroy(gameObject, 10f);
        

        //�������� ����������� ��� ���� 
        //��������� �������, ��� ������
    }
}

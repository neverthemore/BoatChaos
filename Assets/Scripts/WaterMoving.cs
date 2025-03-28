using Unity.VisualScripting;
using UnityEngine;

public class WaterMoving : MonoBehaviour
{
    [Header("��������� �������")]
    
    [SerializeField] private float _maxAngle;
    [SerializeField] private float turnSpeedMultiplier = 1f; // ��������� ��� �������� �������� �������

    [Header("������� �����")]
    [SerializeField] private Transform waterPivot;  // ����� � ����� �������, ������������ ��� ������� �������� (�������� ������ �������)
    //[SerializeField] private Transform waterObject; // ������ ���� (�������� ������� Waves), ������� �������� � ������� �����������

    [Header("��������� �������� ����")]
    [SerializeField] private float _shipSpeed;


    GameObject _plane;
    private float _resultAngle;
    UIStatistic _UIStatistic;    
    [SerializeField] Wheel _wheel;
    void Start()
    {          
        _UIStatistic = GetComponent<UIStatistic>();
        _plane = GameObject.Find("Plane") ;        
    }

    private void Moving()
    {
        Vector3 move = transform.forward * _shipSpeed * Time.deltaTime * -1;
        _plane.transform.localPosition += move;
        _UIStatistic.RemainingDistance -= _shipSpeed * Time.deltaTime;
    }

    private void Rotating()
    {
        float angle = _wheel.GetCurrentAngle();
        _resultAngle = Mathf.Clamp((angle / (1080 / _maxAngle)), -_maxAngle, _maxAngle);        
        transform.eulerAngles = new Vector3(0f, -_resultAngle, 0f);
    }


    void Update()
    {

        //�������� ���� �������� � ��������� �������������� ���� (������������ maxAngle)
        float angle = _wheel.GetCurrentAngle();
        _resultAngle = Mathf.Clamp(angle / (1080 / _maxAngle), -_maxAngle, _maxAngle);
        
        waterPivot.Rotate(0f, -_resultAngle * turnSpeedMultiplier * Time.deltaTime, 0f);
        
        //    ��� ����������� �� �������� �������, ������ ���� ������ �������� ����� ������� ��������� -Z.
        _plane.transform.Translate(Vector3.forward * -_shipSpeed * Time.deltaTime, Space.World);

        _UIStatistic.RemainingDistance -= _shipSpeed * Time.deltaTime;
    }

}

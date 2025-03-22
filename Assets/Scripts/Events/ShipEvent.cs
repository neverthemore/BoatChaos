using UnityEngine;

public abstract class ShipEvent : MonoBehaviour
{
    //����� �����������, ������������ � ��
    [SerializeField] EventData _eventData;
    private bool _isActive;
    //+���-�� ���� ������� �� ��������� ��� �����, � ����������� �� ���

    public virtual void StartEvent()
    {
        if (_eventData.name == "�������� �������")
        {
            Wheel wheel = GetComponentInChildren<Wheel>();
            wheel.SetRotation(false);
        }
        //������ ������ ������ (���������� ��������, ����������, ��������� ��������...)
        //������ �������� ����� ������ ��������� ��������� ��� ������� (�������� �������� ��� �������, ����� � ��)
        _isActive = true;
    }

    public abstract void UpdateEvent();

    public virtual void FinishEvent()
    {
        if (_eventData.name == "�������� �������")
        {
            Wheel wheel = GetComponentInChildren<Wheel>();
            wheel.SetRotation(true);
        }
        //�������������� ���������� (+ �������� ���� ������� ��������)
    }
}

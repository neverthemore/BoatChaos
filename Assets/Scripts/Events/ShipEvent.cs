using UnityEngine;

public abstract class ShipEvent : MonoBehaviour
{
    //����� �����������, ������������ � ��
    [SerializeField] EventData _eventData;
    private bool _isActive;
    //+���-�� ���� ������� �� ��������� ��� �����, � ����������� �� ���

    public virtual void StartEvent()
    {
        
        //������ ������ ������ (���������� ��������, ����������, ��������� ��������...)
        //������ �������� ����� ������ ��������� ��������� ��� ������� (�������� �������� ��� �������, ����� � ��)
        _isActive = true;
    }

    public abstract void UpdateEvent();

    public virtual void FinishEvent()
    {
        //�������������� ���������� (+ �������� ���� ������� ��������)
    }
}

using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
public abstract class Event : MonoBehaviour
{
    //����� �����������, ������������ � ��
    [SerializeField]EventData _eventData;
    public bool _enabled;
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
    }

    public virtual void EndEvent()
    {
        if (_eventData.name == "�������� �������")
        {
            Wheel wheel = GetComponentInChildren<Wheel>();
            wheel.SetRotation(true);
        }
        //�������������� ���������� (+ �������� ���� ������� ��������)
    }

    protected virtual void EndGame()
    {
        //�� ��� ������ �����, ���� ��� ��������
    }


}

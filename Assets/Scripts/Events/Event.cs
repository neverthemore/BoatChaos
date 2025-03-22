using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
public abstract class Event : MonoBehaviour
{
    //����� �����������, ������������ � ��
    EventData _eventData;
    public bool _enabled;
    //+���-�� ���� ������� �� ��������� ��� �����, � ����������� �� ���

    public virtual void StartEvent()
    {
        //������ ������ ������ (���������� ��������, ����������, ��������� ��������...)
        //������ �������� ����� ������ ��������� ��������� ��� ������� (�������� �������� ��� �������, ����� � ��)
    }

    public virtual void EndEvent()
    {
        //�������������� ���������� (+ �������� ���� ������� ��������)
    }

    protected virtual void EndGame()
    {
        //�� ��� ������ �����, ���� ��� ��������
    }


}

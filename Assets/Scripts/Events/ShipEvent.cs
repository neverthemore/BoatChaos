using UnityEngine;

public enum EventState {Inactive, Active, ��mpleted }
public abstract class ShipEvent : ScriptableObject
{
    [Header("Base Settings")]
    [SerializeField]protected EventData _eventData;
    public EventState State { get; protected set; } = EventState.Inactive;

    public virtual bool CanActivate()
    {
        return State == EventState.Inactive;  //���������/���� ����� �������� ���� ��������
    }

    public virtual void Activate()
    {
        State = EventState.Active;
        //������ ������ ������� (�������� ����� ����� �������� � ������� � UI)
    }

    public virtual void Complete()
    {
        State = EventState.Inactive; //��� Complete ������ ��� ������ �����
        //������ ��������� �������
    }
}

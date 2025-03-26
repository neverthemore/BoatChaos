using UnityEngine;

public enum EventState {Inactive, Active}
public abstract class ShipEvent : ScriptableObject
{
    [Header("Base Settings")]
    [SerializeField]public EventData _EventData;
    public EventState State { get; protected set; } = EventState.Inactive;

    public void Initialize()
    {
        State = EventState.Inactive;
    }

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
    }
}

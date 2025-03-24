using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/ Broken Wheel Event")]
public class BrokenWheelEvent : ShipEvent
{
    //����� ��������� � ���, ��� ������� �������� (�������� �����)

    [Header("Wheel Events")]
    public UnityEvent OnWheelBroken;
    public UnityEvent OnWheelFixed;

    public int Amount_For_Fix = 4;
    private int _currentFix = 0;

    public void AddOneFix() //�������� �� ����
    {
        _currentFix++;
        Debug.Log("���� �� ����");
        if (_currentFix == Amount_For_Fix) Complete();
    }

    public override void Activate()
    {
        base.Activate();
        OnWheelBroken?.Invoke();
    }

    public override void Complete()
    {
        base.Complete();
        OnWheelFixed?.Invoke();
    }

};

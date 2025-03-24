using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/ Broken Wheel Event")]
public class BrokenWheelEvent : ShipEvent
{
    //Нужно уведомить о том, что штурвал сломался (наверное ивент)

    [Header("Wheel Events")]
    public UnityEvent OnWheelBroken;
    public UnityEvent OnWheelFixed;

    public int Amount_For_Fix = 4;
    private int _currentFix = 0;

    public void AddOneFix() //Починить на одну
    {
        _currentFix++;
        Debug.Log("Фикс на одну");
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

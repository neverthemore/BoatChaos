using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/ Broken Wheel Event")]
public class BrokenWheelEvent : ShipEvent
{
    //Нужно уведомить о том, что штурвал сломался (наверное ивент)

    [Header("Wheel Events")]
    public UnityEvent OnWheelBroken;
    public UnityEvent OnWheelFixed;

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

using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/ Broken Mast Event")]
public class BrokenMastEvent : ShipEvent
{
    [Header("Mast Events")]
    public UnityEvent OnMastBroken;
    public UnityEvent OnMastFixed;

    public override void Activate()
    {
        base.Activate();
        OnMastBroken?.Invoke();
    }

    public override void Complete()
    {
        base.Complete();
        OnMastFixed?.Invoke();
    }
}

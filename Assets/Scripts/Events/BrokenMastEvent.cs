using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/ Broken Mast Event")]
public class BrokenMastEvent : ShipEvent
{
    [Header("Mast Events")]
    public UnityEvent OnMastBroken;
    public UnityEvent OnMastFixed;

    public int Amount_For_Fix = 4;
    private int _currentFix = 0;

    public void AddOneFix() //Починить на одну
    {
        _currentFix++;
        Debug.Log("Мачта: " + _currentFix + ": " + Amount_For_Fix);
        if (_currentFix >= Amount_For_Fix) Complete();
    }

    public override void Activate()
    {
        base.Activate();
        _currentFix = 0;
        OnMastBroken?.Invoke();
    }

    public override void Complete()
    {
        base.Complete();
        _currentFix = 0;
        OnMastFixed?.Invoke();
    }
}

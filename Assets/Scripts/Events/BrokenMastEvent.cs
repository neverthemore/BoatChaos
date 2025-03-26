using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/ Broken Mast Event")]
public class BrokenMastEvent : ShipEvent
{
    [Header("Mast Events")]
    public UnityEvent OnMastBroken;
    public UnityEvent OnMastFixed;

    public int Amount_For_Fix = 30;
    public int Reduce_Per_seconds = 3;
    private int _currentFix = 0;


    public void AddOneFix() //Нажимать много раз, чтобы выполнить
    {
        _currentFix++;
        Debug.Log("Мачта: " + _currentFix + ": " + Amount_For_Fix);
        if (_currentFix >= Amount_For_Fix) Complete();
    }

    public void ReducePerSecond()
    {
        
        _currentFix -= Reduce_Per_seconds;
        if (_currentFix < 0 ) _currentFix = 0;
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
        OnMastFixed?.Invoke();
        _currentFix = 0;
    }
}

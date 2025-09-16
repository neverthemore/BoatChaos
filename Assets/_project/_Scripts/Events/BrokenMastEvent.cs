using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using ShipGame.Events;


[CreateAssetMenu(menuName = "Events/ Broken Mast Event")]
public class BrokenMastEvent : ShipEvent
{
    public int Amount_For_Fix = 30;
    public int Reduce_Per_seconds = 3;
    private int _currentFix = 0;

    public int Current_Fix => _currentFix;

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

        EventBus<MastBrokenEvent>.Publish(new MastBrokenEvent { Source = this });
    }

    public override void Complete()
    {
        base.Complete();
        _currentFix = 0;

        EventBus<MastFixedEvent>.Publish(new MastFixedEvent { Source = this });
    }
}

//Можно добавить логику например кто починил, на каком корабле и тд
public class MastBrokenEvent
{
    public BrokenMastEvent Source;
}

public class MastFixedEvent
{
    public BrokenMastEvent Source;
}
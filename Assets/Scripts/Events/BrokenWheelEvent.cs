using UnityEngine;
using UnityEngine.Events;
using ShipGame.Events;

[CreateAssetMenu(menuName = "Events/ Broken Wheel Event")]
public class BrokenWheelEvent : ShipEvent
{
    //Нужно уведомить о том, что штурвал сломался (наверное ивент)

    public int Amount_For_Fix = 4;
    private int _currentFix = 0;

    public void AddOneFix() //Починить на одну
    {
        _currentFix++;
        Debug.Log("Штурвал: " + _currentFix + ": " + Amount_For_Fix);
        if (_currentFix >= Amount_For_Fix) Complete();
    }

    public override void Activate()
    {
        base.Activate();
        _currentFix = 0;

        EventBus<WheelBrokenEvent>.Publish(new WheelBrokenEvent { Source = this });
    }



    public override void Complete()
    {
        base.Complete();
        _currentFix = 0;

        EventBus<WheelBrokenEvent>.Publish(new WheelBrokenEvent { Source = this });
    }

};

public class WheelBrokenEvent
{
    public BrokenWheelEvent Source;
}

public class WheelFixedEvent
{
    public BrokenWheelEvent Source;
}

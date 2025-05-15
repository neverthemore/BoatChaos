using UnityEngine;
using UnityEngine.Events;
using ShipGame.Events;


[CreateAssetMenu(menuName = "Events/ Illnes Event")]

public class IllnesEvent : ShipEvent
{
    private bool isAnibodyIll = false;         
    public int numberOfIllCharacter;

    public override void Activate()
    {
        numberOfIllCharacter = Random.Range(1, 3);  //Надо сделать, чтобы был 1
        base.Activate();
        isAnibodyIll = true;

        EventBus<IllnesStartEvent>.Publish(new IllnesStartEvent { Source = this });
    }

    public override void Complete()
    {
        base.Complete();
        isAnibodyIll = false;

        EventBus<IllnesEndEvent>.Publish(new IllnesEndEvent { Source = this });
    }    
}

public class IllnesStartEvent
{
    public IllnesEvent Source;
}

public class IllnesEndEvent
{
    public IllnesEvent Source;
}

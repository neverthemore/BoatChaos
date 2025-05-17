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
        numberOfIllCharacter = Random.Range(0, 2);  //Надо сделать, чтобы был 1
        base.Activate();
        isAnibodyIll = true;

        EventBus<IllnesStartEvent>.Publish(new IllnesStartEvent { Source = this });
    }
    public void OnEnable()
    {
        EventBus<IllnesEndEvent>.Subscribe(EndEvent);
    }

    protected void EndEvent(IllnesEndEvent evt)
    {
        Complete();
    }
    public override void Complete()
    {
        base.Complete();
        isAnibodyIll = false;    
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

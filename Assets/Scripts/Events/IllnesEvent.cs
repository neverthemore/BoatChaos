using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Events/ Illnes Event")]

public class IllnesEvent : ShipEvent
{
    [Header("Mast Events")]
    public UnityEvent OnIllnessStart;
    public UnityEvent OnIllnesEnd;

    private bool isAnibodyIll = false;         
    public int numberOfIllCharacter;

    public override void Activate()
    {
        numberOfIllCharacter = Random.Range(0, 3);
        base.Activate();
        isAnibodyIll = true;
        OnIllnessStart?.Invoke();
    }

    public override void Complete()
    {
        base.Complete();
        isAnibodyIll = false;
        OnIllnesEnd.Invoke();
    }    
}

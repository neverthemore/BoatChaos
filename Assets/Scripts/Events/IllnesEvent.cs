using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Events/ Illnes Event")]

public class IllnesEvent : ShipEvent
{
    [Header("Mast Events")]
    public UnityEvent OnIllnessStart;
    public UnityEvent OnIllnesEnd;

    private int _amountIllness = 0;

    public override void Activate()
    {
        base.Activate();
        _amountIllness = 0;
        OnIllnessStart.Invoke();
    }

    public override void Complete()
    {
        base.Complete();
        OnIllnesEnd.Invoke();

    }

    public void HealOneCharacter()
    {
        _amountIllness += 1;
        if (_amountIllness == 3) Complete();
    }
}

using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/ Enemy Attack Event")]
public class EnemyEvent : ShipEvent
{
    [Header("Enemy Events")]
    public UnityEvent OnEnemyStart;
    public UnityEvent OnEnemyEnd;


    //“ипы подплывают р€дом сзади, затем ровн€ютс€, затем следуют за нами
    //ѕериодически стрел€ют

    public override void Activate()
    {
        base.Activate();
        OnEnemyStart?.Invoke();
    }

    public override void Complete()
    {
        base.Complete();
        OnEnemyEnd?.Invoke();
    }
}

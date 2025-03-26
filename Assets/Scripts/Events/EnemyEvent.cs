using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/ Enemy Attack Event")]
public class EnemyEvent : ShipEvent
{
    [Header("Enemy Events")]
    public UnityEvent OnEnemyStart;
    public UnityEvent OnEnemyEnd;

    //ѕериодически стрел€ют (вызыва€ пробоины)

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

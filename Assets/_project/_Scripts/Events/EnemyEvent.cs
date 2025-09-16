using UnityEngine;
using UnityEngine.Events;
using ShipGame.Events;

[CreateAssetMenu(menuName = "Events/ Enemy Attack Event")]
public class EnemyEvent : ShipEvent
{

    //ѕериодически стрел€ют (вызыва€ пробоины)

    public override void Activate()
    {
        base.Activate();

        EventBus<EnemyAttackEvent>.Publish(new EnemyAttackEvent { Source = this });
    }

    public override void Complete()
    {
        base.Complete();

        EventBus<EnemyEndAttackEvent>.Publish(new EnemyEndAttackEvent { Source = this });
    }
}

public class EnemyAttackEvent
{
    public EnemyEvent Source;
}

public class EnemyEndAttackEvent
{
    public EnemyEvent Source;
}

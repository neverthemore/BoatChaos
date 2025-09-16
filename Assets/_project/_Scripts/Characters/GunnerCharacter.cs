using ShipGame.Events;
using UnityEngine;

public class GunnerCharacter : CrewCharacter
{
    protected override void Update()
    {
        base.Update();
        if (!_isActive)
        {
            animator.SetBool("use", false);
            return; 
        }    
    }
    private void StartAttacking(EnemyAttackEvent evt)
    {
        behavior.BlackboardReference.SetVariableValue("Enemies", true);
    }
    private void StopAttacking(EnemyEndAttackEvent evt)
    {
        behavior.BlackboardReference.SetVariableValue("Enemies", false);
    }
    private void OnEnable()
    {
        EventBus<EnemyAttackEvent>.Subscribe(StartAttacking);
        EventBus<EnemyEndAttackEvent>.Subscribe(StopAttacking);
    }
    private void OnDisable()
    {
        EventBus<EnemyAttackEvent>.Unsubscribe(StartAttacking);
        EventBus<EnemyEndAttackEvent>.Unsubscribe(StopAttacking);
    }
}

using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AgentFire", story: "[Agent] Fires from [Cannon]", category: "Action", id: "02451c74935bdc6b6b2d79e6c66222a5")]
public partial class AgentFireAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Cannon;

    protected override Status OnStart()
    {
        Cannon cannon = Cannon.Value.GetComponent<Cannon>();
        GameObject interactor = Agent.Value;
        cannon.Interact(interactor);
        cannon.Interact(interactor);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}


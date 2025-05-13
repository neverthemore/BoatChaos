using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PickUpHammer", story: "[Agent] PickUps [Hammer]", category: "Action", id: "b714c202561cce3f814795e17a0cb7ad")]
public partial class PickUpHammerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<BaseItem> Hammer;        
    
    protected override Status OnStart()
    {
        BaseItem hammer = Hammer.Value;        
        GameObject character = Agent.Value;
        character.GetComponent<ItemState>().PickUpItem(hammer);
        hammer.Interact(character);
        hammer.PickUp();
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


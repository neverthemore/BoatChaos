using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PickUpItem", story: "[Agent] PickUps [Item]", category: "Action", id: "b714c202561cce3f814795e17a0cb7ad")]
public partial class PickUpItemAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<BaseItem> Item;
    protected override Status OnStart()
    {
        BaseItem item = Item.Value;        
        GameObject character = Agent.Value;
        character.GetComponent<ItemState>().PickUpItem(item);

        if (item != null)
        {
            item.Interact(character);            
            return Status.Success;
        }
        else
        {
            return Status.Failure;
        }
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {

    }
}


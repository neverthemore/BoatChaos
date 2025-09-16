using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DropItem", story: "[Agent] Drops [Item]", category: "Action", id: "b9a98deecd7962aaa5fc5c0c1b70f671")]
public partial class DropItemAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<BaseItem> Item;

    protected override Status OnStart()
    {
        BaseItem item = Item.Value;
        GameObject character = Agent.Value;
        character.GetComponent<ItemState>().DropItem();
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


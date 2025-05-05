using UnityEngine;

public class Hammer : BaseItem
{
    protected override void Start()
    {
        base.Start();
        Name = "Hammer";
        _whoCanEnteract = WhoCanEnteract.Technar;    
    }

    public override void Interact(GameObject interactor)
    {
        base.Interact(interactor);
        if (!IsInteractionAllowed)
            return;

        PickUp();
    }
    protected override void PickUp()
    {
        base.PickUp();
    }

    public override void DropItem()
    {
        base.DropItem();
    }     
    protected override void Update()
    {
        base.Update();
    }
}

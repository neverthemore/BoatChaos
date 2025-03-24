using UnityEngine;

public class Hammer : BaseItem
{
    protected override void Start()
    {
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

    private void PickUp()
    {
        _interactor.GetComponent<BaseCharacter>().AddItem(this);
        GetComponent<Collider>().enabled = false;
    }
}

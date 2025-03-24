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
        Debug.Log("Механик пытается взять");
        PickUp();
    }

    private void PickUp()
    {
        _rb.isKinematic = true;
        _interactor.GetComponent<BaseCharacter>().AddItem(this);
        GetComponent<Collider>().enabled = false;
    }
}

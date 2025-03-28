using UnityEngine;

public class Hammer : BaseItem, IPromtable
{
    //Интерфейс промтабл
    Canvas canvas;

    private bool _isEquip = false;

    protected override void Start()
    {
        base.Start();
        Name = "Hammer";                                
        _whoCanEnteract = WhoCanEnteract.Technar;

        Transform parent = transform.parent;
        canvas = parent.GetComponentInChildren<Canvas>();
        canvas.gameObject.SetActive(false);
    }

    public override void Interact(GameObject interactor)
    {
        base.Interact(interactor);
        if (!IsInteractionAllowed)
            return;
        //Debug.Log("Механик пытается взять");
        PickUp();
    }

    private void PickUp()
    {
        _rb.isKinematic = true;
        _interactor.GetComponent<BaseCharacter>().AddItem(this);
        transform.localRotation = Quaternion.identity;
        GetComponent<Collider>().enabled = false;

        _isEquip = true;
    }

    public override void DropItem()
    {
        _rb.isKinematic = false;
        _isEquip = false;
        GetComponent<Collider>().enabled = true;
    }

    public void ShowPromt()
    {
        if (_isEquip) return;

        canvas.gameObject.SetActive(true);
    }

    public void HidePromt()
    {
        if (_isEquip) return;
        canvas.gameObject.SetActive(false);
    }
}

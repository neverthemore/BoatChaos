using UnityEngine;

public class Hammer : BaseItem, IPromtable
{

    private bool _isEquip = false;
    private bool _isPromtShow = false;

    Canvas canvas;

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
        canvas.gameObject.SetActive(true);
        _isPromtShow=true;
        canvas.transform.LookAt(Camera.main.transform);
    }

    public void HidePromt()
    {
        _isPromtShow = false;
        canvas.gameObject.SetActive(false);
    }

    public bool NeedToShowPromt()
    {
        return !_isEquip && !_isPromtShow;
    }

    private void Update()
    {
        if (_isPromtShow)
        {
            canvas.transform.LookAt(Camera.main.transform);
        }
        if (_isPromtShow && _isEquip)
        {
            HidePromt();
        }
    }
}

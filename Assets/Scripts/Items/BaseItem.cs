using UnityEngine;
using UnityEngine.UIElements;

public enum WhoCanEnteract          //������ ��������    
{
    Captain, Pushkar, Technar, Doctor, All
}

[RequireComponent(typeof(Rigidbody))]
public class BaseItem : MonoBehaviour, IInteractable, IPromtable
{
    private bool _isEquip = false;
    private bool _isPromtShow = false;
    //������ ��� ������
    public string Name; 
    protected Rigidbody _rb;   
    private Canvas canvas;

    protected WhoCanEnteract _whoCanEnteract = WhoCanEnteract.All;
    protected GameObject _interactor;

    protected bool IsInteractionAllowed;
    protected virtual void Start()
    {        
        canvas = transform.GetComponentInChildren<Canvas>();
        canvas.gameObject.SetActive(false);
        _rb = GetComponent<Rigidbody>();
    }
    public virtual void Interact(GameObject interactor)
    {        
        IsInteractionAllowed = false;
        //Debug.Log(_whoCanEnteract.ToString());
        if (_whoCanEnteract != WhoCanEnteract.All)  //���� �� ����� ��������, �� ����� ��������� ��� �� ���� ��������
            if (interactor.GetComponent<BaseCharacter>().CharacterName != _whoCanEnteract.ToString()) return; 

        _interactor = interactor;
        IsInteractionAllowed = true;
        //Debug.Log("��� " + interactor.GetComponent<BaseCharacter>().CharacterName);
    }   
    public virtual void DropItem()
    {
        _rb.isKinematic = false;
        _isEquip = false;
        GetComponentInChildren<Collider>().enabled = true;
    }
    protected virtual void PickUp()
    {
        _rb.isKinematic = true;
        _interactor.GetComponent<BaseCharacter>().AddItem(this);
        transform.localRotation = Quaternion.identity;
        GetComponentInChildren<Collider>().enabled = false;

        _isEquip = true;
    }
    public virtual void UseItem()
    {
        
    }
    public void ShowPromt()
    {
        canvas.gameObject.SetActive(true);
        _isPromtShow = true;
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
    protected virtual void Update()
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

using System.Collections;
using UnityEngine;

public class Ball : BaseItem, IInteractable
{
    //private Rigidbody _rb;  ���� � ��������

    private GameObject _interactor;  //BaseCharacter

    private void Start()
    {
        Name = "CannonBall";
    }
    public void Interact(GameObject interactor)
    {
        if (interactor.GetComponent<BaseCharacter>().CharacterName != "Pushkar") return;        
        _interactor = interactor;
        Debug.Log("��� �������!");
        PickUp();
    }
    public void PickUp()
    {
        _interactor.GetComponent<BaseCharacter>().AddItem(this);
        GetComponent<Collider>().enabled = false;
        //+ ��������� ���������
        /*        
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;        
        _rb.constraints = RigidbodyConstraints.FreezeAll; //��� ���� � �����?
        */
    }

    public void Drop()
    {
        //���� ����� �����-�� ������ ��� ����� ���� ������ ��� ����
    }
    
    public void ChargeBall()
    {
        //������� ����� ���� ������ (!!!�� �������� ������!!!)
        Destroy(gameObject);
    }
}

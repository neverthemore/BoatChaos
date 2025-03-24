using System.Collections;
using UnityEngine;

public class Ball : BaseItem
{
    private void Start()
    {
        Name = "CannonBall";
    }
    public override void Interact(GameObject interactor)
    {
        /*
        if (interactor.GetComponent<BaseCharacter>().CharacterName != "Pushkar") return;        
        _interactor = interactor;
        Debug.Log("��� �������!");
        */
        base.Interact(interactor);
        PickUp();
    }
    public void PickUp()
    {
        _interactor.GetComponent<BaseCharacter>().AddItem(this);
        GetComponent<Collider>().enabled = false;
    }

    public void Drop()
    {
        //���� ����� �����-�� ������ ��� ����� ���� ������ ��� ����
    }
    
    public override void UseItem()
    {
        //������� ����� ���� ������ (!!!�� �������� ������!!!)
        Destroy(gameObject);
    }
}

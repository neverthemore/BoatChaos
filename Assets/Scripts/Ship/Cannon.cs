using Unity.VisualScripting;
using UnityEngine;

public class Cannon : MonoBehaviour, IInteractable
{    
    private bool _ballLoaded = false;


    public void Interact(GameObject interactor)
    {
        if (interactor.GetComponent<BaseCharacter>().CharacterName != "Pushkar") return;
        if (!_ballLoaded) //�� �������� -> ��������
        {
            if (interactor.GetComponent<BaseCharacter>().GetItem() != null)
                if (interactor.GetComponent<BaseCharacter>().GetItem().Name == "CannonBall")
                {
                //������� ��� ����, ����� ��������
                interactor.GetComponent<BaseCharacter>().GetItem().UseItem();
                _ballLoaded = true;
                Debug.Log("���� ���������");
                }
        }
        else
        {
            Fire();
            //��������
        }
    }
    private void Fire()
    {
        Debug.Log("FIRE!!!");
        _ballLoaded = false;
    }       
}

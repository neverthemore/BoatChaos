using Unity.VisualScripting;
using UnityEngine;

public class Cannon : MonoBehaviour, IInteractable
{    
    private bool _ballLoaded;


    public void Interact(GameObject interactor)
    {
        if (interactor.GetComponent<BaseCharacter>().CharacterName != "Pushkar") return;

        if (!_ballLoaded) //�� �������� -> ��������
        {
            if (interactor.GetComponent<BaseCharacter>().GetItem().Name == "CannonBall")
            {
                //������� ��� ����, ����� ��������
                _ballLoaded = true;
                Debug.Log("���� ���������");
            }
        }
        else
        {
            Debug.Log("�������");
            //��������
        }
    }
    public void ChangeState(bool t)
    {
        _ballLoaded = t;
    }
    public void RequestToFire()
    {
        if (_ballLoaded)
        {
            Fire();
            Debug.Log("FIRE!!!");
        }
    }
    private void Fire()
    {
        _ballLoaded = false;
    }       
}

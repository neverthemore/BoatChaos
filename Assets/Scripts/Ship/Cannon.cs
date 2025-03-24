using Unity.VisualScripting;
using UnityEngine;

public class Cannon : MonoBehaviour, IInteractable
{    
    private bool _ballLoaded;


    public void Interact(GameObject interactor)
    {
        if (interactor.GetComponent<BaseCharacter>().CharacterName != "Pushkar") return;

        if (!_ballLoaded) //Не заряжена -> заряжаем
        {
            if (interactor.GetComponent<BaseCharacter>().GetItem().Name == "CannonBall")
            {
                //Удаляем это ядро, пушка заряжена
                _ballLoaded = true;
                Debug.Log("Ядро загружено");
            }
        }
        else
        {
            Debug.Log("Выстрел");
            //Стреляем
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

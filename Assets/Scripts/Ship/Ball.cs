using System.Collections;
using UnityEngine;

public class Ball : BaseItem, IInteractable
{
    //private Rigidbody _rb;  Есть в родителе

    private GameObject _interactor;  //BaseCharacter

    private void Start()
    {
        Name = "CannonBall";
    }
    public void Interact(GameObject interactor)
    {
        if (interactor.GetComponent<BaseCharacter>().CharacterName != "Pushkar") return;        
        _interactor = interactor;
        Debug.Log("Это пушкарь!");
        PickUp();
    }
    public void PickUp()
    {
        _interactor.GetComponent<BaseCharacter>().AddItem(this);
        GetComponent<Collider>().enabled = false;
        //+ коллайдер выключить
        /*        
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;        
        _rb.constraints = RigidbodyConstraints.FreezeAll; //Уже есть в пикАП?
        */
    }

    public void Drop()
    {
        //Если нужна какая-то логика для дропа ядра именно для ядра
    }
    
    public void ChargeBall()
    {
        //Удаляем нахуй этот объект (!!!не проебать ошибки!!!)
        Destroy(gameObject);
    }
}

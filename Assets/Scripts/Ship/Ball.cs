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
        Debug.Log("Это пушкарь!");
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
        //Если нужна какая-то логика для дропа ядра именно для ядра
    }
    
    public override void UseItem()
    {
        //Удаляем нахуй этот объект (!!!не проебать ошибки!!!)
        Destroy(gameObject);
    }
}

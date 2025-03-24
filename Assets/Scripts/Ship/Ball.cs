using System.Collections;
using UnityEngine;

public class Ball : BaseItem
{
    protected override void Start()
    {
        base.Start();
        Name = "CannonBall";
        _whoCanEnteract = WhoCanEnteract.Pushkar;
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
        _rb.isKinematic = true; // Отключаем физику
        _interactor.GetComponent<BaseCharacter>().AddItem(this);
        GetComponent<Collider>().enabled = false;
    }

    private void Drop()
    {
        //Если нужна какая-то логика для дропа ядра именно для ядра
    }
    
    public override void UseItem()
    {
        //Удаляем нахуй этот объект (!!!не проебать ошибки!!!)
        Destroy(gameObject);
    }
}

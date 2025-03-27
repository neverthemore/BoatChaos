using UnityEngine;
using UnityEngine.UIElements;

public enum WhoCanEnteract          //Список названий    
{
    Captain, Pushkar, Technar, Doctor, All
}

[RequireComponent(typeof(Rigidbody))]
public class BaseItem : HintInterract, IInteractable
{
    //Скрипт для шмоток
    public string Name; //МБ нужно будет делать Scriptable
    protected Rigidbody _rb;

    protected WhoCanEnteract _whoCanEnteract = WhoCanEnteract.All;
    protected GameObject _interactor;

    protected bool IsInteractionAllowed;

    public virtual void Interact(GameObject interactor)
    {        
        IsInteractionAllowed = false;
        //Debug.Log(_whoCanEnteract.ToString());
        if (_whoCanEnteract != WhoCanEnteract.All)  //Если не любой персонаж, то далее проверяем тот ли этот пресонаж
            if (interactor.GetComponent<BaseCharacter>().CharacterName != _whoCanEnteract.ToString()) return; 

        _interactor = interactor;
        IsInteractionAllowed = true;
        Debug.Log("Это " + interactor.GetComponent<BaseCharacter>().CharacterName);
    }

    protected override void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody>();
    }

    public virtual void DropItem()
    {

    }
    
    public virtual void UseItem()
    {
        //Логика использования, например уничтожение объекта (Засунуть ядро в аушку например)
    }
}

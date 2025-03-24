using UnityEngine;
using UnityEngine.UIElements;

public enum WhoCanEnteract
{
    Captain, Pushkar, Mechanic, Doctor, All
}

[RequireComponent(typeof(Rigidbody))]
public class BaseItem : MonoBehaviour, IInteractable
{
    //Скрипт для шмоток
    public string Name; //МБ нужно будет делать Scriptable
    protected Rigidbody _rb;

    protected WhoCanEnteract _whoCanEnteract;
    protected GameObject _interactor;

    public virtual void Interact(GameObject interactor)
    {
        if (_whoCanEnteract != WhoCanEnteract.All)
            if (interactor.GetComponent<BaseCharacter>().CharacterName != _whoCanEnteract.ToString()) return; 
        _interactor = interactor;
        Debug.Log("Это " + interactor.GetComponent<BaseCharacter>().CharacterName);
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    public virtual void UseItem()
    {
        //Логика использования, например уничтожение объекта
    }
}

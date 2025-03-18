using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    [SerializeField] protected string _characterName;
    //[SerializeField] protected GameObject characterPrefab; //Ќе факт, что нужно

    protected bool _isActive;

    protected virtual void Update()
    {
        if (!_isActive)
        {
            AIMod();
            return;
        }

    }
    
    public virtual void Activate()
    {
        _isActive = true;
        //+ логика в наследнике
    }

    public virtual void Deactivate()
    {
        _isActive = false;
        //+ логика
    }

    protected virtual void AIMod()
    {

    }
}

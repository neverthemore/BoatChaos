using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class BaseItem : MonoBehaviour
{
    //Скрипт для шмоток
    public string Name; //МБ нужно будет делать Scriptable
    protected Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    public virtual void UseItem()
    {
        //Логика использования, например уничтожение объекта
    }
}

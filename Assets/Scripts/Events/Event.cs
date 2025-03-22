using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
public abstract class Event : MonoBehaviour
{
    //Ивент запускается, прекращается и тд
    [SerializeField]EventData _eventData;
    public bool _enabled;
    //+Что-то типо отсчета до проигрыша или урона, в зависимости че там

    public virtual void StartEvent()
    {
        if (_eventData.name == "Сломаный штурвал")
        {
            Wheel wheel = GetComponentInChildren<Wheel>();
            wheel.SetRotation(false);
        }
        //Логика начала ивента (отключение штурвала, управление, понижение скорости...)
        //Вообще возможно нужно делать отдельные менеджеры для ивентов (например менеджер для пробоин, мачты и тд)
    }

    public virtual void EndEvent()
    {
        if (_eventData.name == "Сломаный штурвал")
        {
            Wheel wheel = GetComponentInChildren<Wheel>();
            wheel.SetRotation(true);
        }
        //Соответственно завершение (+ наверное надо сделать проверку)
    }

    protected virtual void EndGame()
    {
        //Хз как делать будем, надо еще подумать
    }


}

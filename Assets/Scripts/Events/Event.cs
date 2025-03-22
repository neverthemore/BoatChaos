using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
public abstract class Event : MonoBehaviour
{
    //Ивент запускается, прекращается и тд
    EventData _eventData;
    //+Что-то типо отсчета до проигрыша или урона, в зависимости че там

    public virtual void StartEvent()
    {
        //Логика начала ивента (отключение штурвала, управление, понижение скорости...)
        //Вообще возможно нужно делать отдельные менеджеры для ивентов (например менеджер для пробоин, мачты и тд)
    }

    public virtual void EndEvent()
    {
        //Соответственно завершение (+ наверное надо сделать проверку)
    }

    protected virtual void EndGame()
    {
        //Хз как делать будем, надо еще подумать
    }


}

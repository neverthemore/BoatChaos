using UnityEngine;

public abstract class ShipEvent : MonoBehaviour
{
    //Ивент запускается, прекращается и тд
    [SerializeField] EventData _eventData;
    private bool _isActive;
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
        _isActive = true;
    }

    public abstract void UpdateEvent();

    public virtual void FinishEvent()
    {
        if (_eventData.name == "Сломаный штурвал")
        {
            Wheel wheel = GetComponentInChildren<Wheel>();
            wheel.SetRotation(true);
        }
        //Соответственно завершение (+ наверное надо сделать проверку)
    }
}

using UnityEngine;

public enum EventState {Inactive, Active, Соmpleted }
public abstract class ShipEvent : ScriptableObject
{
    [Header("Base Settings")]
    [SerializeField]protected EventData _eventData;
    public EventState State { get; protected set; } = EventState.Inactive;

    public virtual bool CanActivate()
    {
        return State == EventState.Inactive;  //Приоритет/шанс можно наверное сюда добавить
    }

    public virtual void Activate()
    {
        State = EventState.Active;
        //Логика начала события (например можно промт показать и вывести в UI)
    }

    public virtual void Complete()
    {
        State = EventState.Inactive; //Или Complete смотря как ивенты будут
        //Логика звершения события
    }
}

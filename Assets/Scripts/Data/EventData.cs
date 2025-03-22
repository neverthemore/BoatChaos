using UnityEngine;

[CreateAssetMenu(fileName = "NewEventData", menuName = "Event Data/ New Event")]
public class EventData : ScriptableObject
{
    //»нфа о ивенте (чтобы создать экземпл€р класса нужно создать —криптаблќбж)
                                                    //ћаксим, если будут вопросы как создать Scriptsble, пиши
    public string _name;
    public string _description;
    public bool _enabled;
    public int _priority;
}

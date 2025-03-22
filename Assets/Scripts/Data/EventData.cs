using UnityEngine;

[CreateAssetMenu(fileName = "NewEventData", menuName = "Event Data/ New Event")]
public class EventData : ScriptableObject
{
    //»нфа о ивенте (чтобы создать экземпл€р класса нужно создать —криптаблќбж)
    public string _name;
    [TextArea(3, 8)]
    public string _description;    
    public int _priority;
}

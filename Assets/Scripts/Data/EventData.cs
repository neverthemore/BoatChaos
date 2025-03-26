using UnityEngine;

[CreateAssetMenu(fileName = "NewEventData", menuName = "Event Data/ New Event")]
public class EventData : ScriptableObject
{
    public string _name;
    [TextArea(3, 8)]
    public string _description;    
    public int _priority;
}

using UnityEngine;

[CreateAssetMenu(fileName = "NewEventData", menuName = "Event Data/ New Event")]
public class EventData : ScriptableObject
{
    //���� � ������ (����� ������� ��������� ������ ����� ������� ������������)
                                                    //������, ���� ����� ������� ��� ������� Scriptsble, ����
    public string _name;
    public string _description;
    public bool _enabled;
    public int _priority;
}

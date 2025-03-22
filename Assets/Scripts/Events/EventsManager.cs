using NUnit.Framework;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;
using System.Collections.Generic;

public class EventsManager : MonoBehaviour
{
    public static EventsManager Instance;

    [SerializeField] private Event[] events; //�������� ����� �������
    private List<ShipEvent> _activeEvents = new();

    [SerializeField]float timerDuration = 10f;
    float timeRemaining;
    bool isTimerRunning = false;
    int _totalPriority = 0;

    private void Awake() => Instance = this;
    

    void Start()
    {
        //StartTimer(); � ������ ����� (�������� ��� ������ ���� (����� �������� � ��))
        
    }

    private void StartTimer()
    {
        timeRemaining = timerDuration;
        isTimerRunning = true;
    }
       
    public void StartChosenEvent(ShipEvent name) 
    {
        Debug.Log("Starting event: " + name);
        //����� ����� ��� ����� Event
        name.StartEvent(); //�������� ��� ����� ������� � �������
    }

    /*
    public void ChooseEvent()
    {
        foreach (Event e in events)
        {
            _totalPriority += e._priority;
        }
        int randowValue = Random.Range(0, _totalPriority);
        int currentPriority = 0;
        foreach (Event e in events)
        {
            currentPriority += e._priority;
            if (randowValue < currentPriority)
            {
                StartChosenEvent(e);
                break;
            }
        }
    }
    */

    void Update()
    {
        if (isTimerRunning)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                isTimerRunning = false;
                StartTimer();
            }
        }
    }
}

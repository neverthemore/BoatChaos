using NUnit.Framework;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;
using System.Collections.Generic;

public class EventsManager : MonoBehaviour
{
    public static EventsManager Instance;

    [SerializeField] private List<ShipEvent> _allEvents = new List<ShipEvent>();

    [SerializeField]float timerDuration = 10f;

    float timeRemaining;
    bool isTimerRunning = false;
    int _totalPriority = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    

    void Start()
    {
        //StartTimer(); В другом месте (например при начале игры (после обучения и тд))
        foreach (ShipEvent e in _allEvents)
        {
            e.Activate();
        }
        
    }

    private void StartTimer()
    {
        timeRemaining = timerDuration;
        isTimerRunning = true;
    }
       
    public void StartChosenEvent(ShipEvent name) 
    {
        Debug.Log("Starting event: " + name);
        //Старт через сам класс Event
        //name.StartEvent(); //Например вот старт первого в массиве
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

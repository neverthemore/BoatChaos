using NUnit.Framework;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;
using System.Collections.Generic;

public class EventsManager : MonoBehaviour
{
    public static EventsManager Instance;

    [SerializeField] private List<ShipEvent> _allEvents;

    [SerializeField]float timerDuration = 10f;

    float timeRemaining;
    bool isTimerRunning = false;
    int _totalPriority = 0;

    bool _isEventActivated = false;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    

    void Start()
    {
        InitializeEvents();

        StartTimer(); //В другом месте (например при начале игры (после обучения и тд))       
    }

    private void InitializeEvents()
    {
        foreach (ShipEvent e in _allEvents) //Чтобы в начале игры ScriptableObj не сохранил параметры с прошлого раза
        {
            e.Initialize();
        }
    }

    private void StartTimer()
    {
        timeRemaining = timerDuration;
        isTimerRunning = true;
    }
       
    public void StartChosenEvent(ShipEvent name) 
    {
        //Debug.Log("Starting event: " + name._EventData._name);
        name.Activate();
    }

    
    public void ChooseEvent()
    {
        _totalPriority = 0;
        foreach (var ev in _allEvents) //Сумируем вес только если ивент не запущен
        {
            if (ev.CanActivate()) _totalPriority += ev._EventData._priority;           
        }

        int randowValue = Random.Range(0, _totalPriority);
        int currentPriority = 0;

        foreach (var ev in _allEvents)
        {
            currentPriority += ev._EventData._priority;

            if (randowValue < currentPriority && ev.CanActivate())  //Проверка, чтобы ивент был неактивен
            {
                StartChosenEvent(ev);
                _isEventActivated = true;
                break;
            }
        }

        if (!_isEventActivated) //Если ивент не запустился, то запускаем таймер повторно
        {
            Debug.Log("Ивент не активирован");
            StartTimer();
        }
        _isEventActivated = false;
    }
   

    void Update()
    {
        if (isTimerRunning)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                isTimerRunning = false;

                ChooseEvent();
                StartTimer();
                
            }
        }
    }
}

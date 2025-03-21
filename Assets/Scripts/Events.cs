using System.Xml.Serialization;
using UnityEngine;

public class Events : MonoBehaviour
{
    public static Events Instance;

    bool brokenWheel; int brokenWheelPriority = 35;
    bool holes; int holesPriority = 55;
    bool brokenMast; int brokenMastPriority = 40;
    bool illnes; int illnesPriority = 10;
    bool stress; int stressPriority = 0;
    bool hangover; int hangoverPriority = 25;
    bool enemies; int enemiesPriority = 30;

    float timerDuration = 10f;
    float timeRemaining;
    bool isTimerRunning = false;

    void Start()
    {
        StartTimer();
        brokenWheel = false;
        holes = false;
        brokenMast = false;
        illnes = false;
        stress = false;
        hangover = false;
        enemies = false;
    }

    private void StartTimer()
    {
        timeRemaining = timerDuration;
        isTimerRunning = true;
    }
       
    public void ChooseEvent()
    {
        
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
                StartTimer();
            }
        }
    }
}

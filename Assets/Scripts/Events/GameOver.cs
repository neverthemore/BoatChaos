using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/ GameOver")]
public class GameOver: ScriptableObject
{
    //Говорим, что игра закончилась
    public UnityEvent OnGameOver;
    public UnityEvent OnGameVictory;   
}

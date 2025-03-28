using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/ GameOver")]
public class GameOver: ScriptableObject
{
    public UnityEvent OnGameOver;

    public void StartGameOver()
    {
       
    }
}

using System.Collections;
using UnityEngine;

public class EnemyShipManager : MonoBehaviour
{
    public static EnemyShipManager Instance; //Хз пока что

    [SerializeField] private EnemyEvent _enemyEvent;


    [SerializeField] GameObject _enemyShipPrefab;

    [SerializeField] Vector3 _spawnOffset = new Vector3(50, 0, -150);

    [SerializeField] Vector3 _goalOffset = new Vector3(50, 0, 0); //Оффет от корабля (корабль стоит в 0 0 0)

    private void OnEnable()
    {
        _enemyEvent.OnEnemyStart.AddListener(SpawnShip);
    }

    private void OnDisable()
    {
        _enemyEvent.OnEnemyStart.RemoveListener(SpawnShip);
    }

    private void Start()
    {
        Instance = this;

        //SpawnShip();
    }

    private void SpawnShip()
    {
        Debug.Log("Появился вражеский корабль");
        GameObject enemyShip = Instantiate(_enemyShipPrefab, _spawnOffset, Quaternion.identity);

        EnemyShip movement = enemyShip.GetComponent<EnemyShip>();
        if (movement != null)
        {
            movement.SetOffset(_goalOffset);
        }
    }
    
}

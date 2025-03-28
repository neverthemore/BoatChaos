using System.Collections;
using UnityEngine;

public class EnemyShipManager : MonoBehaviour
{
    public static EnemyShipManager Instance; //’з пока что

    [SerializeField] private EnemyEvent _enemyEvent;


    [SerializeField] GameObject _enemyShipPrefab;

    [SerializeField] Vector3 _spawnOffset = new Vector3(50, 0, -150);

    [SerializeField] Vector3 _goalOffset = new Vector3(50, 0, 0); //ќффет от корабл€ (корабль стоит в 0 0 0)

    [SerializeField]Canvas _canvas;

    GameObject _enemyShip;
    bool _wasSpawning = false;

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
        _canvas.gameObject.SetActive(false);
        //SpawnShip();
    }

    private void Update()
    {
        if (!_wasSpawning) return;
        CheckForEndEvent();
    }

    private void CheckForEndEvent()
    {
        if (_enemyShip == null)
        {
            _enemyEvent.Complete();
            _wasSpawning = false;
        }
    }

    private void SpawnShip()
    {
        _enemyShip = Instantiate(_enemyShipPrefab, _spawnOffset, Quaternion.identity);
        _wasSpawning = true;

        EnemyShip movement = _enemyShip.GetComponent<EnemyShip>();
        if (movement != null)
        {
            movement.SetOffset(_goalOffset);
        }
    }
    
    private void ShowPromt()
    {
        _canvas.gameObject.SetActive(true);
    }
    private void HidePromt()
    {
        _canvas.gameObject.SetActive(false);
    }
}

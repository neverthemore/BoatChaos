using System.Collections;
using UnityEngine;
using ShipGame.Events;

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
        EventBus<EnemyAttackEvent>.Subscribe(SpawnShip);
    }

    private void OnDisable()
    {
        EventBus<EnemyAttackEvent>.Unsubscribe(SpawnShip);
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
            HidePromt();
        }
    }

    private void SpawnShip(EnemyAttackEvent evt)
    {
        _enemyShip = Instantiate(_enemyShipPrefab, _spawnOffset, Quaternion.identity);
        _wasSpawning = true;

        EnemyShip movement = _enemyShip.GetComponent<EnemyShip>();
        if (movement != null)
        {
            movement.SetOffset(_goalOffset);
        }
        ShowPromt();
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

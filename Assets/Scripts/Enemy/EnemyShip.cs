using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [SerializeField] GameObject _enemyShipPrefab;

    [SerializeField] Vector3 _offset = new Vector3(50, 0, -150);


   private void SpawnShip()
    {
        Instantiate(_enemyShipPrefab, Ship.LastShipPosition + _offset, Ship.LastShipRotation);
    }

    //Затем этот корабль должен быстренько поравняться с нашим кораблем (быть параллельно ему)
}

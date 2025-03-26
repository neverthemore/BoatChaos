using UnityEngine;

public class EnemyShipManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyShipPrefab;

    [SerializeField] Vector3 _offset = new Vector3(50, 0, -150);


    private void SpawnShip()
    {
        GameObject enemyShip = Instantiate(_enemyShipPrefab, Ship.LastShipPosition + _offset, Ship.LastShipRotation);
    }
}

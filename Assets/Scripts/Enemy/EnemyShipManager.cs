using System.Collections;
using UnityEngine;

public class EnemyShipManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyShipPrefab;

    [SerializeField] Vector3 _offset = new Vector3(50, 0, -150);

    private void Start()
    {
        StartCoroutine(Spa());
    }

    private void SpawnShip()
    {
         // ������� ������� � ������ �������� �������� ���������
        Vector3 spawnPosition = Ship.LastShipPosition + Ship.LastShipRotation * _offset;
        GameObject enemyShip = Instantiate(_enemyShipPrefab, spawnPosition, Ship.LastShipRotation);
        //GameObject enemyShip = Instantiate(_enemyShipPrefab, Ship.LastShipPosition + _offset, Ship.LastShipRotation);

        // �������� �������� � ������ ��������
        EnemyShip movement = enemyShip.GetComponent<EnemyShip>();
        if (movement != null)
        {
            movement.SetOffset(_offset);
        }
    }

    private IEnumerator Spa()
    {
        Debug.Log("����� ��������� �������");
        yield return new WaitForSeconds(3);
        SpawnShip();

    }
}

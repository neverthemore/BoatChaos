using UnityEngine;

public class Iceberg : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private GameObject wavesObject; // Ссылка на объект Waves
    private UIStatistic _wavesStats;

    void Awake()
    {
        // Находим объект Waves и получаем компонент
        if (wavesObject == null)
        {
            wavesObject = GameObject.Find("Waves");
        }

        if (wavesObject != null)
        {
            _wavesStats = wavesObject.GetComponent<UIStatistic>();
            if (_wavesStats == null)
            {
                Debug.LogError("UIStatistic не найден на объекте Waves!");
            }
        }
        else
        {
            Debug.LogError("Объект Waves не найден в сцене!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ship") && other is CapsuleCollider)
        {
            Debug.Log("Столкновение с кораблем!");

            if (_wavesStats != null)
            {
                _wavesStats.ShipHP -= _damage;
                Debug.Log($"Новое HP: {_wavesStats.ShipHP}");
            }

            Destroy(gameObject);
        }
    }
}
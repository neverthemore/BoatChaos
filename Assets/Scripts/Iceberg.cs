using UnityEngine;

public class Iceberg : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private GameObject wavesObject; // ������ �� ������ Waves
    private UIStatistic _wavesStats;

    void Awake()
    {
        // ������� ������ Waves � �������� ���������
        if (wavesObject == null)
        {
            wavesObject = GameObject.Find("Waves");
        }

        if (wavesObject != null)
        {
            _wavesStats = wavesObject.GetComponent<UIStatistic>();
            if (_wavesStats == null)
            {
                Debug.LogError("UIStatistic �� ������ �� ������� Waves!");
            }
        }
        else
        {
            Debug.LogError("������ Waves �� ������ � �����!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ship") && other is CapsuleCollider)
        {
            Debug.Log("������������ � ��������!");

            if (_wavesStats != null)
            {
                _wavesStats.ShipHP -= _damage;
                Debug.Log($"����� HP: {_wavesStats.ShipHP}");
            }

            Destroy(gameObject);
        }
    }
}
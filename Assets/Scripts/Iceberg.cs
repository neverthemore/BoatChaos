using UnityEngine;

public class Iceberg : MonoBehaviour
{    
    [SerializeField] private float _damage;
    private void OnTriggerEnter(Collider other)
    {        
        GameObject obj = other.gameObject;
        if (obj.tag == "Ship")
        {            
            obj.GetComponent<UIStatistic>().ShipHP -= _damage;
            Destroy(gameObject);
        }
    }     
}

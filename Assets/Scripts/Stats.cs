using UnityEngine;

public class Stats : MonoBehaviour
{
    public float ShipHP;
    public float RemainingDistanse;
    void Start()
    {
        RemainingDistanse = 1000f;
        ShipHP = 100f;
    }
    
    void Update()
    {
        if (RemainingDistanse <= 0)
        {
            Debug.Log("777 BIG WIN 777");
        }
        if (ShipHP <= 0)
        {
            Debug.Log("LOSE");
        }
    }
}

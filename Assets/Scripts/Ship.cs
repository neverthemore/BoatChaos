using Unity.VisualScripting;
using UnityEngine;

public class Ship : MonoBehaviour
{
    Wheel wheel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wheel = FindAnyObjectByType<Wheel>();
    }

    // Update is called once per frame
    void Update()
    {
        float angle = wheel.GetCurrentAngle();
    }
}

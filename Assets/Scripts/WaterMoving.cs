using Unity.VisualScripting;
using UnityEngine;

public class WaterMoving : MonoBehaviour
{    
    [SerializeField] private float _shipSpeed;
    [SerializeField] private float _maxAngle;
    GameObject _plane;
    private float _resultAngle;
    UIStatistic _UIStatistic;    
    [SerializeField] Wheel _wheel;
    void Start()
    {          
        _UIStatistic = GetComponent<UIStatistic>();
        _plane = GameObject.Find("Plane") ;        
    }

    private void Moving()
    {
        Vector3 move = transform.forward * _shipSpeed * Time.deltaTime * -1;
        _plane.transform.localPosition += move;
        _UIStatistic.RemainingDistance -= _shipSpeed * Time.deltaTime;
    }

    private void Rotating()
    {
        float angle = _wheel.GetCurrentAngle();
        _resultAngle = Mathf.Clamp((angle / (1080 / _maxAngle)), -_maxAngle, _maxAngle);
        transform.eulerAngles = new Vector3(0f, -_resultAngle, 0f);
    }


    void Update()
    {
        Moving();
        Rotating();
    }
}

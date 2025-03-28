using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private BrokenMastEvent _mastBroken;
    Wheel _wheel;
    UIStatistic UIStatistic;
    CharacterController controller;

    private float _resultAngle;
    private float _incline = 0f; //Наклон
    private bool _rightIncline = true;

    private float _shipAngle;
    [SerializeField] private float _maxAngle = 24f;
    [SerializeField] private float _maxIncline = 10f;
    [SerializeField] public float _shipSpeed = 2f;  
    private float _brokenMastCoef = 1.5f; //Коэф сломаной мачты

    [SerializeField]private bool _isMoving = true;
    [SerializeField] private bool _isRotate = true;

    private static Vector3 _lastShipPosition;
    private static Quaternion _lastShipRotation;

    public static Vector3 LastShipPosition { get { return _lastShipPosition; } }
    public static Quaternion LastShipRotation {  get { return _lastShipRotation; } }

        private void OnEnable()
    {
        _mastBroken.OnMastBroken.AddListener(SetBrokenMastParameters);
        _mastBroken.OnMastFixed.AddListener(SetNormalMastParameters);
    }

    void Start()
    {
        UIStatistic = GetComponent<UIStatistic>();
        _wheel = FindAnyObjectByType<Wheel>();
        controller = GetComponent<CharacterController>();
    }
    private void SetBrokenMastParameters() 
    { 
        _maxIncline *= _brokenMastCoef; 
        _shipSpeed /= _brokenMastCoef;
        Debug.Log("Мачта сломана");
    }
    private void SetNormalMastParameters() 
    { 
        _maxIncline /= _brokenMastCoef;
        _shipSpeed *= _brokenMastCoef;
        Debug.Log("Мачта починена");
    }    

    private void RotateShip()
    {
        /*float angle = _wheel.GetCurrentAngle();
        _resultAngle = Mathf.Clamp((angle / (1080 / _maxAngle)), -_maxAngle, _maxAngle);
        transform.localEulerAngles = new Vector3(0f, _resultAngle, 0f);*/

        float deltaIncline;        
        deltaIncline = 1 * Time.deltaTime * 4;        
        Mathf.Clamp(deltaIncline, 0.2f, 0.5f);
        _incline += deltaIncline * ((_rightIncline) ? 1f : -1f);
        if (_incline >= _maxIncline) _rightIncline = false;
        else if (_incline <= -_maxIncline) _rightIncline = true;
        
        transform.localEulerAngles = new Vector3(0f, 0f, _incline);
    }

    void Update()
    {        
        if (_isRotate)
        {
            RotateShip();
        }

        _lastShipPosition = transform.position;
        _lastShipRotation = transform.rotation;

    }

    public void SinkShip()
    {

    }

   
}

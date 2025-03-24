using Unity.VisualScripting;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private BrokenMastEvent _mastBroken;
    Wheel wheel;
    CharacterController controller;

    private float _incline = 0f; //Наклон
    private bool _rightIncline = true;

    private float _shipAngle;
    [SerializeField] private float _maxAngle = 24f;
    [SerializeField] private float _maxIncline = 10f;
    [SerializeField] private float _shipSpeed = 2f;  
    private float _brokenMastCoef = 1.5f; //Коэф сломаной мачты

    private void OnEnable()
    {
        _mastBroken.OnMastBroken.AddListener(SetBrokenMastParameters);
        _mastBroken.OnMastFixed.AddListener(SetNormalMastParameters);
    }

    void Start()
    {
        wheel = FindAnyObjectByType<Wheel>();
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
        
    private void MoveShip()
    {
        Vector3 move = transform.forward * _shipSpeed * Time.deltaTime;
        controller.Move(move);
    }

    private void RotateShip()
    {
        float angle = wheel.GetCurrentAngle();
        _shipAngle = Mathf.Clamp(angle / (1080 / _maxAngle), -_maxAngle, _maxAngle);
        float deltaIncline;
        deltaIncline = _shipSpeed * Time.deltaTime * 4;
        Mathf.Clamp(deltaIncline, 0.2f, 0.5f);
        _incline += deltaIncline * ((_rightIncline) ? 1f : -1f);
        if (_incline >= _maxIncline) _rightIncline = false;
        else if (_incline <= -_maxIncline) _rightIncline = true;
        
        transform.localEulerAngles = new Vector3(0f, _shipAngle, _incline);
    }

    void Update()
    {
        //MoveShip();

        RotateShip();
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class Ship : MonoBehaviour
{
    Wheel wheel;
    CharacterController controller;

    private float _incline = 0f;
    private bool _rightIncline = true;
    private float _shipSpeed = 2f;
    private float _maxAngle = 24f;
    private float _maxIncline = 10f;
    private float _brokenMastCoef = 1.5f;

    public void SetBrokenMastParameters() { 
        _maxIncline *= _brokenMastCoef; 
        _shipSpeed /= _brokenMastCoef;
    }
    public void SetNormalMastParameters() { 
        _maxIncline /= _brokenMastCoef;
        _shipSpeed *= _brokenMastCoef;
    }
    
    void Start()
    {
        wheel = FindAnyObjectByType<Wheel>();
        controller = GetComponent<CharacterController>();
    }

    private void MoveShip()
    {
        Vector3 move = new Vector3(0f, 0f, _shipSpeed);
        controller.Move(move);
    }

    private void RotateShip()
    {
        float angle = wheel.GetCurrentAngle();
        float shipAngle = Mathf.Clamp(angle / (1080 / _maxAngle), -_maxAngle, _maxAngle);
        float deltaIncline;
        if (_incline > 0.05f || _incline < -0.05f)
        {
            deltaIncline = (_shipSpeed / Mathf.Abs(_incline)) * Time.deltaTime;
        }
        else
        {
            deltaIncline = 0.05f;
        }
        Mathf.Clamp(deltaIncline, 0.05f, 0.1f);
        _incline += deltaIncline * ((_rightIncline) ? 1f : -1f);
        if (_incline >= _maxIncline) _rightIncline = false;
        else if (_incline <= -_maxIncline) _rightIncline = true;        

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, shipAngle, _incline);
    }

    void Update()
    {
        //MoveShip();

        RotateShip();
    }
}

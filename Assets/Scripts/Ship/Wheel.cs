using Unity.VisualScripting;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    private bool _canRotate = false;
    private float _currentAngle = 0f;
    private float _angularSpeed = 90f;

    InputSystem_Actions inputActions;
    Transform _wheel;

    public void SetBrokenWheelParameters()
    {
        _canRotate = false;
    }
    public void SetNormalWheelParameters()
    {
        _canRotate = true;
    }

    public void SetRotation(bool t)
    {
        _canRotate = t;
    }
    private void Start()
    {
        _wheel = GameObject.Find("Wheell").transform;
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
    }
    void Update()
    {
        if (_canRotate)
        {
            float input = inputActions.Captain.Manage.ReadValue<float>();

            if (input > 0) _currentAngle += _angularSpeed * Time.deltaTime;
            else if (input < 0) _currentAngle -= _angularSpeed * Time.deltaTime;
            _currentAngle = Mathf.Clamp(_currentAngle, -1080f, 1080f);

            _wheel.localEulerAngles = new Vector3(-_currentAngle, -90f, 90f);
        }        
    }
    public float GetCurrentAngle() { return _currentAngle; }
}

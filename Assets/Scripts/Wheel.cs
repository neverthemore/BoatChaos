using UnityEngine;

public class Wheel : MonoBehaviour
{
    private bool _canRotate = false;
    float _currentAngle = 0f;
    float _angularSpeed = 90f;
    InputSystem_Actions inputActions;

    private void Start()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
    }
    void Update()
    {
        float input = inputActions.Captain.Manage.ReadValue<float>();
        
        if (input > 0) _currentAngle += _angularSpeed * Time.deltaTime;
        else if (input < 0) _currentAngle -= _angularSpeed * Time.deltaTime;  
        _currentAngle = Mathf.Clamp(_currentAngle, -1080f, 1080f);

        transform.localEulerAngles = new Vector3(-_currentAngle, -90f, 90f);
    }
    public float GetCurrentAngle() { return _currentAngle; }
}

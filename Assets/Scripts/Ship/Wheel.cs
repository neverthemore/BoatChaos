using Unity.VisualScripting;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private BrokenWheelEvent _brokenWheelEvent;

    #region Private Variables
    private bool _canRotate = false;
    private bool _isBroken = false;

    private float _currentAngle = 0f;
    private float _angularSpeed = 90f;
    #endregion

    InputSystem_Actions inputActions;
    Transform _wheel;

    private void OnEnable()
    {
        _brokenWheelEvent.OnWheelBroken.AddListener(SetBrokenWheelParameters);
        _brokenWheelEvent.OnWheelFixed.AddListener(SetNormalWheelParameters);

    }

    private void OnDisable()
    {
        _brokenWheelEvent.OnWheelBroken.RemoveListener(SetBrokenWheelParameters);
        _brokenWheelEvent.OnWheelFixed.RemoveListener(SetNormalWheelParameters);
    }

    private void Start()
    {
        _wheel = GameObject.Find("Wheel").transform;
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
    }

    public void SetBrokenWheelParameters()
    {
        //_isBroken = true;
        Debug.Log("Штурвал сломан!");
    }
    public void SetNormalWheelParameters()
    {
        _isBroken = false;
        Debug.Log("Штурвал починен!");
    }

    public void SetRotation(bool t) //Скрипт для капитана
    {
        _canRotate = t;
    }

    
    void Update()
    {
        if (_canRotate && !_isBroken)
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

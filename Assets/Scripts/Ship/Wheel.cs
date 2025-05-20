using ShipGame.Events;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class Wheel : MonoBehaviour, IFixable
{

    [SerializeField] private BrokenWheelEvent _brokenWheelEvent;

    [SerializeField]Canvas canvas;
    bool _isPromtShow;
    Slider slider;

    float _currentFix;
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
        EventBus<WheelBrokenEvent>.Subscribe(SetBrokenWheelParameters);
        EventBus<WheelFixedEvent>.Subscribe(SetNormalWheelParameters);
    }

    private void OnDisable()
    {
        EventBus<WheelBrokenEvent>.Unsubscribe(SetBrokenWheelParameters);
        EventBus<WheelFixedEvent>.Unsubscribe(SetNormalWheelParameters);
    }

    private void Start()
    {
        _wheel = GameObject.Find("Wheel").transform;
        inputActions = new InputSystem_Actions();
        inputActions.Enable();

        Transform parent = transform.parent;      
        slider = canvas.GetComponentInChildren<Slider>();
        HidePromt();
    }

    public void SetBrokenWheelParameters(WheelBrokenEvent evt)
    {
        _isBroken = true;
        ShowPromt();
        _currentFix = 0;
        Debug.Log("Штурвал сломан!");
    }
    public void SetNormalWheelParameters(WheelFixedEvent evt)
    {
        _isBroken = false;
        HidePromt();
        Debug.Log("Штурвал починен!");
    }

    public void SetRotation(bool t) //Скрипт для капитана
    {
        _canRotate = t;
    }

    
    void Update()
    {
        float input = inputActions.Captain.Manage.ReadValue<float>();
        if (_canRotate && !_isBroken)
        {           
            if (input > 0) _currentAngle += _angularSpeed * Time.deltaTime;
            else if (input < 0) _currentAngle -= _angularSpeed * Time.deltaTime;
            _currentAngle = Mathf.Clamp(_currentAngle, -1080f, 1080f);

            _wheel.localEulerAngles = new Vector3(_currentAngle, 90f, 0f);
        }

        if (_isPromtShow)
        {
            canvas.transform.LookAt(Camera.main.transform);
            //canvas.transform.localEulerAngles = new Vector3(0, 0, Mathf.Sin(Time.time * 3f) * 2f);
            slider.value = _currentFix;
        }

        //Автоматическое возвращение штурвала в начальное состояние
        if (input == 0 || _isBroken)
        {
            if (_currentAngle > 0) _currentAngle -= _angularSpeed * Time.deltaTime;
            if (_currentAngle < 0) _currentAngle += _angularSpeed * Time.deltaTime;
        }
    }
    public float GetCurrentAngle() { return _currentAngle; }

    public void StartFix()
    {
        _brokenWheelEvent.AddOneFix();
        _currentFix += 0.25f;
    }

    public bool NeedToFix()
    {
        return _isBroken;
    }

    public void ShowPromt()
    {
        if (_isPromtShow && !_isBroken) return;
        canvas.gameObject.SetActive(true);
        _isPromtShow = true;
        canvas.transform.LookAt(Camera.main.transform);
    }

    public void HidePromt()
    {
        if (_isBroken) return;
        _isPromtShow = false;
        canvas.gameObject.SetActive(false);
    }

    public bool NeedToShowPromt()
    {
        return _isBroken && !_isPromtShow;
    }
}

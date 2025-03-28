using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Wheel : MonoBehaviour, IFixable, IPromtable
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

        Transform parent = transform.parent;
        slider = canvas.GetComponentInChildren<Slider>();
    }

    public void SetBrokenWheelParameters()
    {
        _isBroken = true;
        _currentFix = 0;
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

            _wheel.localEulerAngles = new Vector3(_currentAngle, 90f, 0f);
        }
        slider.value = _currentFix;
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
        canvas.gameObject.SetActive(true);
        _isPromtShow = true;
        canvas.transform.LookAt(Camera.main.transform);
    }

    public void HidePromt()
    {
        _isPromtShow = false;
        canvas.gameObject.SetActive(false);
    }

    public bool NeedToShowPromt()
    {
        return _isBroken && !_isPromtShow;
    }
}

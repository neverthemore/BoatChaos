using UnityEngine;
using UnityEngine.UI;

public class ShipMast : MonoBehaviour, IFixable
{
    //Чисто скрипт для поломки мачты
    [SerializeField] private BrokenMastEvent _brokenMastEvent;

    [SerializeField] Canvas canvas;
    bool _isPromtShow;
    Slider slider;

    private float _currentClickReduceCooldown = 0f;
    private bool _isBroken;

    private void OnEnable()
    {
        _brokenMastEvent.OnMastBroken.AddListener(BreakTheMast);
        _brokenMastEvent.OnMastFixed.AddListener(FixTheMast);

    }

    private void OnDisable()
    {
        _brokenMastEvent.OnMastBroken.RemoveListener(BreakTheMast);
        _brokenMastEvent.OnMastFixed.RemoveListener(FixTheMast);
        
    }

    private void Start()
    {
        Transform parent = transform.parent;
        slider = canvas.GetComponentInChildren<Slider>();
        HidePromt();
    }

    private void Update()
    {
        if (_isBroken)
        {
            _currentClickReduceCooldown -= Time.deltaTime;

            if (_currentClickReduceCooldown < 0f)
            {
                _brokenMastEvent.ReducePerSecond();
                _currentClickReduceCooldown = 1f;
            }
        }
        if (_isPromtShow)
        {
            canvas.transform.LookAt(Camera.main.transform);
        }
        slider.value = (_brokenMastEvent.Current_Fix);
    }

    private void BreakTheMast()
    {
        _isBroken = true;
        ShowPromt();
        Debug.Log("Мачта сломана");
    }

    private void FixTheMast()
    {
        _isBroken = false;
        HidePromt();
        Debug.Log("Мачта починена");
    }

    public void StartFix()
    {
        _brokenMastEvent.AddOneFix();
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

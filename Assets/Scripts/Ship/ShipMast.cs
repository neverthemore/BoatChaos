using UnityEngine;

public class ShipMast : MonoBehaviour, IFixable
{
    //Чисто скрипт для поломки мачты
    [SerializeField] private BrokenMastEvent _brokenMastEvent;

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
    }

    private void BreakTheMast()
    {
        _isBroken = true;
        Debug.Log("Мачта сломана");
    }

    private void FixTheMast()
    {
        _isBroken = false;
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
}

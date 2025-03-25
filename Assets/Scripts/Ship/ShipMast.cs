using UnityEngine;

public class ShipMast : MonoBehaviour, IFixable
{
    //����� ������ ��� ������� �����
    [SerializeField] private BrokenMastEvent _brokenMastEvent;

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

    private void BreakTheMast()
    {
        _isBroken = true;
        Debug.Log("����� �������");
    }

    private void FixTheMast()
    {
        _isBroken = false;
        Debug.Log("����� ��������");
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

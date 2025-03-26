using System.Drawing;
using UnityEngine;

public class MechanicCharacter : CrewCharacter
{
    [SerializeField] private float _fixRange = 2f;

    // ��������� ���������� ��� ������������ ����������� ��������� ������
    private bool _wasAttackPressedLastFrame = false;
    //���� � ����� �������, �� �� ����� ������ ���, ������� ����� ������ ��������� �������
    //��������� ���, ������� ��������� ���, ���� �� ������� � ������� �� ���� �� ���� ������� (�� ��� ������ ������ ��������)
    private float _secondsForFix = 1f;
    private float _clampedSeconds = 0f;


    private IFixable _currentFixable;

    protected override void Update()
    {
        base.Update();
        if (_isIll || !_isActive) return; //��������� ���-���� ������


        // ���������� ��� ������������ ��������� ������
        bool isAttackPressed = inputActions.Crew.Attack.IsPressed();
        bool isAttackTriggered = isAttackPressed && !_wasAttackPressedLastFrame;

        if (GetItem()?.Name == "Hammer")
        {
            bool isFixableHit = CastRayForFixAndCheck();

            if (isFixableHit)
            {
                // ��������� Wheel (���������)
                if (_currentFixable is Wheel)
                {
                    if (isAttackPressed)
                    {
                        _clampedSeconds += Time.deltaTime;
                    }

                    // ��� ���������� ������� �����
                    if (_clampedSeconds >= _secondsForFix)
                    {
                        _currentFixable.StartFix();
                        _clampedSeconds = 0f;
                    }
                }
                // ��������� ShipMast (��������� �������)
                else if (_currentFixable is ShipMast)
                {
                    if (isAttackTriggered)
                    {
                        _currentFixable.StartFix();
                    }
                }
            }
            else
            {
                _clampedSeconds = 0f;
            }
        }
        else
        {
            _clampedSeconds = 0f;
        }

        // ��������� ��������� ������ ��� ���������� �����
        _wasAttackPressedLastFrame = isAttackPressed;
        
    }

    private bool CastRayForFixAndCheck()   //������ ���, ������ � IFixable -> ���������� StartFix()
    {
        Ray ray = new Ray(cmCamera.transform.position, cmCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _fixRange))
        {
            IFixable fixable = hit.collider.GetComponent<IFixable>();
            if (fixable != null)
            {
                if (fixable.NeedToFix())
                {
                    _currentFixable = fixable;
                    return true;
                }
            }
            else _currentFixable = null;
        }
        return false;
    }
}

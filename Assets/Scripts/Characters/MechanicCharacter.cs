using System.Drawing;
using UnityEngine;

public class MechanicCharacter : CrewCharacter
{
    [SerializeField] private float _fixRange = 2f;


    //���� � ����� �������, �� �� ����� ������ ���, ������� ����� ������ ��������� �������
    //��������� ���, ������� ��������� ���, ���� �� ������� � ������� �� ���� �� ���� ������� (�� ��� ������ ������ ��������)
    private float _secondsForFix = 1f;
    private float _clampedSeconds = 0f;

    private IFixable _currentFixable;

    protected override void Update()
    {
        if (!_isActive) return;
        base.Update();

        //��� ���������� ������, ��-�� ����, ��� �� ����� ��������� ���� ������ - �� � �������� ������ ���� ������ �����?

        if (inputActions.Crew.Attack.IsPressed() && GetItem()?.Name == "Hammer")  
        {
            if (CastRayForFixAndCheck())
            { 
                _clampedSeconds += Time.deltaTime;
                Debug.Log("�����");
            }
            else _clampedSeconds = 0;
        }
        else _clampedSeconds = 0f;

        if (_clampedSeconds >= _secondsForFix)
        {
            _currentFixable.StartFix();
            _clampedSeconds = 0f;
        }
        
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

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

    private AudioSource _audioSource;
    private bool _isAudioPlay = false;

    private bool _wasBreachOpened = false;

    private IFixable _currentFixable;

    protected override void Start()
    {
        base.Start();
        _audioSource = GetComponent<AudioSource>();
    }

    protected override void Update()
    {
        base.Update();
        if (!_isActive)
        {
            animator.SetBool("use", false);
            return; //��������� ���-���� ������
        }


        // ���������� ��� ������������ ��������� ������
        bool isAttackPressed = inputActions.Crew.Attack.IsPressed();
        bool isAttackTriggered = isAttackPressed && !_wasAttackPressedLastFrame;

        animator.SetBool("use", isAttackPressed);


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
                    if (inputActions.Crew.Attack.triggered)
                    {
                        _currentFixable.StartFix();
                        //Debug.Log("���� �����");
                    }
                }
                // ��������� �������
                else if (_currentFixable is Breach)
                {
                    //Debug.Log("�������� �� ��������");
                    if (inputActions.Crew.Use.triggered && !_wasBreachOpened)
                    {

                        //Debug.Log("�������� ������");
                        //��������� UI
                        _currentFixable.StartFix();
                        _wasBreachOpened = true;                       
                    }
                    else if (inputActions.Crew.Use.triggered && _wasBreachOpened)
                    {
                        UIBranch.Instance.CloseUI();
                        if (UIBranch.Instance.Success) 
                        {
                            UIBranch.Instance.FixBreach(GetCurrentObj()); 
                        }
                        _wasBreachOpened = false;

                    }
                }
            }
            else
            {
                _clampedSeconds = 0f;

                if (_wasBreachOpened)
                {
                    UIBranch.Instance.CloseUI();
                    if (UIBranch.Instance.Success)
                    {
                        UIBranch.Instance.FixBreach(GetCurrentObj());
                    }
                    _wasBreachOpened = false;
                }
            }

            /*
            if (inputActions.Crew.Attack.IsPressed())
            {
                if (!_isAudioPlay)
                {
                    _isAudioPlay = true;
                    _audioSource.Play();
                }
            }
            else
            {
                if (_isAudioPlay)
                {
                    _isAudioPlay = false;
                    _audioSource.Stop();
                }
            }
            */
        }
        else
        {
            _clampedSeconds = 0f;

            if (_wasBreachOpened)
            {
                UIBranch.Instance.CloseUI();
                if (UIBranch.Instance.Success)
                {
                    UIBranch.Instance.FixBreach(GetCurrentObj());
                }
                _wasBreachOpened = false;
            }
        }

        // ��������� ��������� ������ ��� ���������� �����
        _wasAttackPressedLastFrame = isAttackPressed;
        
    }

    public void PlayHammerSound()
    {
        _audioSource.Play();
    }

    private GameObject GetCurrentObj()
    {
        Ray ray = new Ray(cmCamera.transform.position, cmCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _fixRange))
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    private bool CastRayForFixAndCheck()   //������ ���, ������ � IFixable -> ���������� StartFix()
    {
        Ray ray = new Ray(cmCamera.transform.position, cmCamera.transform.forward);
        RaycastHit hit;

        //Debug.DrawRay(ray.origin, ray.direction * _fixRange, UnityEngine.Color.red);

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

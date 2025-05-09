using System.Drawing;
using UnityEngine;

public class MechanicCharacter : CrewCharacter
{
    [SerializeField] private float _fixRange = 2f;

    // Добавляем переменную для отслеживания предыдущего состояния кнопки
    private bool _wasAttackPressedLastFrame = false;
    //Если в руках молоток, То мы можем нажать ЛКМ, который будет чинить сломанные объекты
    //Выпускает луч, который проверяет тег, пока он выпущен и наведен на цель то цель чинится (ну или логика внутри предмета)
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
            return; //Перестает что-либо делать
        }


        // Переменные для отслеживания состояния кнопки
        bool isAttackPressed = inputActions.Crew.Attack.IsPressed();
        bool isAttackTriggered = isAttackPressed && !_wasAttackPressedLastFrame;

        animator.SetBool("use", isAttackPressed);


        if (GetItem()?.Name == "Hammer")
        {
            bool isFixableHit = CastRayForFixAndCheck();

            if (isFixableHit)
            {
                // Обработка Wheel (удержание)
                if (_currentFixable is Wheel)
                {
                    if (isAttackPressed)
                    {
                        _clampedSeconds += Time.deltaTime;
                    }

                    // При достижении времени чиним
                    if (_clampedSeconds >= _secondsForFix)
                    {
                        _currentFixable.StartFix();
                        _clampedSeconds = 0f;
                    }
                }
                // Обработка ShipMast (одиночные нажатия)
                else if (_currentFixable is ShipMast)
                {
                    if (inputActions.Crew.Attack.triggered)
                    {
                        _currentFixable.StartFix();
                        //Debug.Log("Фикс мачты");
                    }
                }
                // Обработка пробоин
                else if (_currentFixable is Breach)
                {
                    //Debug.Log("Навелись на протечку");
                    if (inputActions.Crew.Use.triggered && !_wasBreachOpened)
                    {

                        //Debug.Log("Пытаемся чинить");
                        //Открываем UI
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

        // Обновляем состояние кнопки для следующего кадра
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

    private bool CastRayForFixAndCheck()   //Зажато ЛКМ, попало в IFixable -> вызывается StartFix()
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

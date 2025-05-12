using NUnit.Framework.Constraints;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrewCharacter : BaseCharacter
{
    #region Components
    protected CharacterController controller;           //Контроллер
    protected Animator animator;                        //Анимации
    private Ship _ship;                                 //Скрипт корабля
    #endregion

    #region Base Variables
    public bool inAiMod;    
    private float _speedOfMoving = 5f;
    private float _jumpUp;
    private float _gravityForce = -5f;
    private bool _isFirstMove = true;
    private Vector3 _lastShipPosition = Ship.LastShipPosition;
    private Quaternion _lastShipRotation = Ship.LastShipRotation;
    #endregion

    [SerializeField]private AudioSource _audioSource;
    [SerializeField] private float pitchVariation = 0.1f; // Величина изменения питча

    override protected void Start()
    {
        base.Start();       

        _ship = GetComponentInParent<Ship>();        
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();        
        inputActions.Enable();
    }
    protected override void RotateCamera()
    {
        base.RotateCamera();

        cmCameraGameObject.transform.localEulerAngles = new Vector3(mouseY, 0f, 0f);
        transform.localEulerAngles = new Vector3(0f, mouseX, 0f);
    }
    public override void Activate()
    {
        base.Activate();
        controller = GetComponent<CharacterController>();
        controller.enabled = true;               
        inAiMod = false;        
    }
    public override void Deactivate()
    {
        base.Deactivate();
        controller = GetComponent<CharacterController>();
        controller.enabled = false;        
        inAiMod = true;        
    }        
    protected override void Update()
    {
        base.Update();
        AnimationsOfMoving();
        if (_isActive)
        {            
            Move();
            RotateCamera();
        }          
    }    
    public void PlaySound()
    {
        if (_audioSource == null) return;
        _audioSource.Stop();
        _audioSource.pitch = 1f + Random.Range(-pitchVariation, pitchVariation);
        _audioSource.Play();
    }
    private void Move()
    {
        if (!controller.isGrounded)
            _jumpUp += _gravityForce * Time.deltaTime;
        else if (_jumpUp <= 0) _jumpUp = 0;

        Vector3 shipDelta = Ship.LastShipPosition - _lastShipPosition;
        Quaternion shipRotationDelta = Ship.LastShipRotation * Quaternion.Inverse(_lastShipRotation);

        if (_isFirstMove)
        {
            shipDelta = Vector3.zero;
            shipRotationDelta = new Quaternion(0, 0, 0, 0);
            _isFirstMove = false;
        }
         
        Vector3 rotatedPosition = shipRotationDelta * (transform.position - _lastShipPosition);
        Vector3 shipMove = (rotatedPosition + _lastShipPosition + shipDelta) - transform.position;

        Vector2 direction = inputActions.Crew.Move.ReadValue<Vector2>();
        Vector3 characterMove = transform.TransformDirection(
            new Vector3(direction.x, 0, direction.y)) * _speedOfMoving * Time.deltaTime;

        Vector3 totalMove = shipMove + characterMove;
        totalMove.y += _jumpUp;
        controller.Move(totalMove);

        _lastShipPosition = Ship.LastShipPosition;
        _lastShipRotation = Ship.LastShipRotation;        
    }
    private void AnimationsOfMoving()
    {
        if (inputActions.Crew.Move.ReadValue<Vector2>() == Vector2.zero)
            animator.SetBool("walking", false);
        animator.SetBool("walking", true);
    }
}

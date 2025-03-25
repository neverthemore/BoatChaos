using NUnit.Framework.Constraints;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrewCharacter : BaseCharacter
{

    protected CharacterController controller;     
    protected Animator animator;
    protected AI ai;

    public bool inAiMod;

    private float _speedOfMoving = 5f;
    private float _jumpUp;
    private float _gravityForce = -5f;
    //Команда, может двигаться и ходить в отличие от кэпа

    private Vector3 _lastShipPosition = Vector3.zero;
    private Quaternion _lastShipRotation = new Quaternion(0,0,0,0);


    override protected void Start()
    {
        base.Start();       
        ai = GetComponent<AI>();
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
        cmCameraGameObject = GameObject.Find("CM Camera" + _characterName);
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
        inAiMod = false;
    }
    public override void Deactivate()
    {
        base.Deactivate();
        inAiMod = true;
    }

    protected override void Update()
    {
        if (_isActive)
        {
            base.Update();

            Move();

            RotateCamera();            
        }
        else
        {
            AIMod();
        }
        
    }
    protected override void AIMod()
    {
        base.AIMod();
        if (ai._isOnPoint)
        {
            ai._isOnPoint = false;
            StartCoroutine(ai.AIMoving());
        }
    }
    private void Move()
    {
        if (!controller.isGrounded)        
            _jumpUp += _gravityForce * Time.deltaTime;
        else if (_jumpUp <= 0) _jumpUp = 0;

        // 1. Вычисляем движение от корабля
        Vector3 shipDelta = Ship.LastShipPosition - _lastShipPosition;
        Quaternion shipRotationDelta = Ship.LastShipRotation * Quaternion.Inverse(_lastShipRotation);

        // 2. Применяем вращение корабля к персонажу
        Vector3 rotatedPosition = shipRotationDelta * (transform.position - _lastShipPosition);
        Vector3 shipMove = (rotatedPosition + _lastShipPosition + shipDelta) - transform.position;

        // 3. Движение от игрока
        Vector2 direction = inputActions.Crew.Move.ReadValue<Vector2>();
        Vector3 characterMove = transform.TransformDirection(
            new Vector3(direction.x, 0, direction.y)) * _speedOfMoving * Time.deltaTime;

        // 4. Комбинируем перемещения
        Vector3 totalMove = shipMove + characterMove;
        totalMove.y += _jumpUp;

        // 5. Применяем движение
        controller.Move(totalMove);

        /*
        //Вычисляем изменение позиции и вращения корабля
        Vector3 deltaPosition = Ship.LastShipPosition - _lastShipPosition;
        Quaternion deltaRotation = Ship.LastShipRotation * Quaternion.Inverse(_lastShipRotation);
        // Применяем вращение к персонажу
        Vector3 rotatedOffset = deltaRotation * (transform.position - Ship.LastShipPosition);
        Vector3 targetPosition =  Ship.LastShipPosition + rotatedOffset;
        // Вычисляем необходимое перемещение
        Vector3 shipMove = targetPosition - transform.position + deltaPosition;


        Vector2 direction = inputActions.Crew.Move.ReadValue<Vector2>();
        Vector3 characterMove = transform.forward * direction.y + transform.right * direction.x;
        characterMove *= _speedOfMoving * Time.deltaTime;

        // Комбинируем оба перемещения
        Vector3 totalMove = shipMove + characterMove;
        totalMove.y = _jumpUp;

        // Применяем движение
        controller.Move(totalMove);
        */
        /*
        //Vector3 move = new Vector3();
        move = transform.forward * direction.y + transform.right * direction.x;
        move *= _speedOfMoving * Time.deltaTime;
        move.y = _jumpUp;
        controller.Move(move);
        */
        // Обновляем предыдущие значения
        _lastShipPosition = Ship.LastShipPosition;
        _lastShipRotation = Ship.LastShipRotation;

        //анимация
        if (direction != Vector2.zero)
        {
            animator.SetBool("walking", true);
        }
        else animator.SetBool("walking", false);
    }
}

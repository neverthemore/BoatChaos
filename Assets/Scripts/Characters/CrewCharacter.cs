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

    private bool _isNeedToStopCoroutine = true;
    private bool _isNeedToSwitchOnNavMesh = false;

    private float _speedOfMoving = 5f;
    private float _jumpUp;
    private float _gravityForce = -5f;

    private bool _isFirstMove = true;

    private Ship _ship;
    //Команда, может двигаться и ходить в отличие от кэпа
    private Vector3 _lastShipPosition = Ship.LastShipPosition;
    private Quaternion _lastShipRotation = Ship.LastShipRotation;


    override protected void Start()
    {
        base.Start();       
        _ship = GetComponentInParent<Ship>();
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
        controller = GetComponent<CharacterController>();
        base.Activate();        
        inAiMod = false;
        controller.enabled = true;
    }
    public override void Deactivate()
    {
        controller = GetComponent<CharacterController>();
        base.Deactivate();
        inAiMod = true;
        controller.enabled = false;
    }

    protected override void Update()
    {
        base.Update();
        if (_isActive)
        {
            if (_isNeedToStopCoroutine)
            {
                ai.StopAllCoroutines();
                ai.SetNavMesh(false);

                _isNeedToStopCoroutine = false;
                _isNeedToSwitchOnNavMesh = true;
            }   

                Move();

                RotateCamera();         
            
        }
        else
        {
            if (_isNeedToSwitchOnNavMesh)
            {
                ai.SetNavMesh(true);
                _isNeedToSwitchOnNavMesh = false;
                _isNeedToStopCoroutine = true;
                ai.ChangePointState(true);
                _isFirstMove = true;
            }
            AIMod();
        }

        if (GetItem() != null)
        {
            animator.SetBool("carring", true);
        }
        else animator.SetBool("carring", false);

    }
    protected override void AIMod()
    {
        if (ai._isOnPoint)
        {
            ai.ChangePointState(false);
            ai._isOnPoint = false;
            StartCoroutine(ai.AIMoving());
        }
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

        if (direction != Vector2.zero)
        {
            animator.SetBool("walking", true);
        }
        else animator.SetBool("walking", false);
    }
}

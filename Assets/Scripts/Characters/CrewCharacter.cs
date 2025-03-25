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

    private Ship _ship;
    //Команда, может двигаться и ходить в отличие от кэпа
    private Vector3 _lastShipPosition = Vector3.zero;
    private Quaternion _lastShipRotation = new Quaternion(0, 0, 0, 0);


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

        if (GetItem() != null)
        {
            animator.SetBool("carring", true);
        }
        else animator.SetBool("carring", false);

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

        Vector3 shipDelta = Ship.LastShipPosition - _lastShipPosition;
        Quaternion shipRotationDelta = Ship.LastShipRotation * Quaternion.Inverse(_lastShipRotation);

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

        //��������
        if (direction != Vector2.zero)
        {
            animator.SetBool("walking", true);
        }
        else animator.SetBool("walking", false);
    }
}

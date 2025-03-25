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
    //Команда, может двигаться и ходить в отличие от кэпа

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
        base.Update();
        if (_isActive)
        {
            if (_isNeedToStopCoroutine)
            {
                ai.StopAllCoroutines(); //Офф корутины
                ai.SetNavMesh(false);   //Офф NavAgent

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
            }
            AIMod();
        }
        
    }
    protected override void AIMod()
    {
        
        if (ai._isOnPoint)
        {
            ai.ChangePointState(false);
            StartCoroutine(ai.AIMoving());
        }
    }
    private void Move()
    {
        if (!controller.isGrounded)        
            _jumpUp += _gravityForce * Time.deltaTime;
        else if (_jumpUp <= 0) _jumpUp = 0;

        Vector2 direction = inputActions.Crew.Move.ReadValue<Vector2>();        
        Vector3 move = new Vector3();
        move = transform.forward * direction.y + transform.right * direction.x;
        move *= _speedOfMoving * Time.deltaTime;


        move.y = _jumpUp;
        controller.Move(move);

        //анимация
        if (direction != Vector2.zero)
        {
            animator.SetBool("walking", true);
        }
        else animator.SetBool("walking", false);
    }    
}

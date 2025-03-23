using NUnit.Framework.Constraints;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrewCharacter : BaseCharacter
{
    CharacterController controller;
    //protected InputSystem_Actions inputActions;
    float _speedOfMoving = 5f;
    Animator animator;
    //Команда, может двигаться и ходить в отличие от кэпа

    override protected void Start()
    {
        base.Start();        
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
    }
    public override void Deactivate()
    {
        base.Deactivate();
    }

    protected override void Update()
    {
        if (_isActive)
        {
            base.Update();

            Move();

            RotateCamera();            
        }
        
    }  
    private void Move()
    {
        Debug.Log("move working");        
        Vector2 direction = inputActions.Crew.Move.ReadValue<Vector2>();
        if (direction != Vector2.zero)
        {
            animator.SetBool("walking", true);
        }
        else animator.SetBool("walking", false);
        Vector3 move = new Vector3();
        move = transform.forward * direction.y + transform.right * direction.x;
        move *= _speedOfMoving * Time.deltaTime;
        controller.Move(move);  
    }    
}

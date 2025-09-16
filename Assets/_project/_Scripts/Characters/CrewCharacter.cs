using NUnit.Framework.Constraints;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class CrewCharacter : BaseCharacter
{
    #region Components
    protected CharacterController controller;           //Контроллер
    protected Animator animator;                        //Анимации                                    
    private NavMeshAgent _agent;
    #endregion

    #region Base Variables
    public bool inAiMod;    
    private float _speedOfMoving = 5f;
    private float _jumpUp;
    private float _gravityForce = -5f;    
    #endregion

    [SerializeField]private AudioSource _audioSource;
    [SerializeField] private float pitchVariation = 0.1f; // Величина изменения питча

    override protected void Start()
    {
        base.Start();
          
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        _agent = GetComponent<NavMeshAgent>();
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
        _agent = GetComponent<NavMeshAgent>();
        _agent.enabled = false;
        controller.enabled = true;               
        inAiMod = false;        
    }
    public override void Deactivate()
    {
        base.Deactivate();
        controller = GetComponent<CharacterController>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.enabled = true;
        controller.enabled = false;        
        inAiMod = true;        
    }        
    protected override void Update()
    {
        base.Update();        
        if (_isActive)
        {            
            Move();
            AnimationsOfMoving();
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
        {
            _jumpUp += _gravityForce * Time.deltaTime;
            Debug.Log("Персонаж падает");
        }
        else if (_jumpUp <= 0) _jumpUp = 0;      

        Vector2 direction = inputActions.Crew.Move.ReadValue<Vector2>();
        Vector3 characterMove = transform.TransformDirection(
            new Vector3(direction.x, 0, direction.y)) * _speedOfMoving * Time.deltaTime;

        Vector3 totalMove = characterMove;
        totalMove.y += _jumpUp;
        if (direction != Vector2.zero)
            controller.Move(totalMove);         
    }
    private void AnimationsOfMoving()
    {
        if (inputActions.Crew.Move.ReadValue<Vector2>() == Vector2.zero)
            animator.SetBool("walking", false);
        animator.SetBool("walking", true);
    }
}

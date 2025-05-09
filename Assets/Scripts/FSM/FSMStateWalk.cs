using UnityEngine;
using UnityEngine.InputSystem;

public class FSMStateWalk : FSMStateBase
{
    private CharacterController _controller;
    private Transform _transform;
    private InputSystem_Actions _inputActions;

    private float _speedOfMoving = 5f;
    private float _jumpUp;
    private float _gravityForce = -5f;
    private bool _isFirstMove = true;
    
    private Vector3 _lastShipPosition = Ship.LastShipPosition;
    private Quaternion _lastShipRotation = Ship.LastShipRotation;

    public FSMStateWalk(
        FSM Fsm, 
        CharacterController controller, 
        Transform transform,
        InputSystem_Actions inputActions) : base(Fsm)
    {
        _controller = controller;
        _transform = transform;
        _inputActions = inputActions;
    }

    public override void Enter()
    {
        Debug.Log("Walk State ENTER");        
    }
    public override void Exit()
    {
        Debug.Log("Walk State EXIT");        
    }
    public override void Update()
    {
        if (_inputActions.Crew.Move.ReadValue<Vector2>() == Vector2.zero)
            Fsm.SetState<FSMStateIdle>();

        Move();
    }
    private void Move()
    {
        if (!_controller.isGrounded)
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

        Vector3 rotatedPosition = shipRotationDelta * (_transform.position - _lastShipPosition);
        Vector3 shipMove = (rotatedPosition + _lastShipPosition + shipDelta) - _transform.position;

        Vector2 direction = _inputActions.Crew.Move.ReadValue<Vector2>();
        Vector3 characterMove = _transform.TransformDirection(
            new Vector3(direction.x, 0, direction.y)) * _speedOfMoving * Time.deltaTime;

        Vector3 totalMove = shipMove + characterMove;
        totalMove.y += _jumpUp;
        _controller.Move(totalMove);

        _lastShipPosition = Ship.LastShipPosition;
        _lastShipRotation = Ship.LastShipRotation;
    }
}

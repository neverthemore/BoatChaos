using UnityEngine;

public class FSMCrew : MonoBehaviour
{
    private FSM _fsm;
    private CharacterController _characterController;
    private InputSystem_Actions _inputActions;

    private void Start()
    {
        _fsm = new FSM();
        _inputActions = new InputSystem_Actions();

        _fsm.AddState(new FSMStateIdle(_fsm));        
        _fsm.AddState(new FSMStateWalk(_fsm, _characterController, transform, _inputActions));        

        _fsm.SetState<FSMStateIdle>();
    }
    private void Update()
    {
        

        _fsm.Update();
    }    
}

using UnityEngine;

public abstract class FSMState
{
    protected readonly FSM Fsm;
    public FSMState(FSM Fsm)
    {
        this.Fsm = Fsm;
    }

    public virtual void Enter() 
    {
        
    }
    public virtual void Exit() { }
    public virtual void Update() { }    
}

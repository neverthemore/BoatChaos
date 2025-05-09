using System.Collections.Generic;
using UnityEngine;
using System;

public class FSM
{
    private FSMState CurrentState { get; set; }

    private Dictionary<Type, FSMState> _states = new Dictionary<Type, FSMState> ();

    public void AddState(FSMState state)
    {
        _states.Add(state.GetType(), state);
    }
    public void SetState<T>() where T : FSMState
    {
        var type = typeof(T);
        if (CurrentState?.GetType() == type)
        {
            return;
        }
        if (_states.TryGetValue(type, out var newState))
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
    public void Update()
    {
        CurrentState?.Update();
    }
}

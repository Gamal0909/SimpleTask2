using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public IState _CurrentState;

    protected Dictionary<System.Type, IState> _StatesTable;

    void Update()
    {
        _CurrentState.LogicUpdate();
    }

    void FixedUpdate()
    {
        _CurrentState.PhysicsUpdate();
    }

    protected void SwitchOn(IState _NewState)
    {
        _CurrentState = _NewState;
        _CurrentState.Enter();
    }

    public void SwitchState(IState _NewState)
    {
        _CurrentState.Exit();
        SwitchOn(_NewState);
    }

    public void SwitchState(System.Type _NewStateType)
    {
        SwitchState(_StatesTable[ _NewStateType]);
    }
}

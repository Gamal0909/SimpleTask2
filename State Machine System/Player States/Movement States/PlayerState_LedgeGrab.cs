using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState_LedgeGrab", menuName = "Data/StateMachine/PlayerStates/LedgeGrab")]
public class PlayerState_LedgeGrab : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        _PlayerController.SetUseGravity(false);
        _PlayerController.SetVelocity(Vector3.zero);

        Debug.Log("Ledge Grab");
    }

    public override void Exit()
    {
        base.Exit();
        _PlayerController.SetUseGravity(true);
    }

    public override void LogicUpdate()
    {
        if (_PlayerInput.Jump)
        {
            _StateMachine.SwitchState(typeof(PlayerState_Jump));
        }
        if (_PlayerInput.Move)
        {
            _StateMachine.SwitchState(typeof(PlayerState_Fall));
        }
    }
    public override void PhysicsUpdate()
    {

    }
}

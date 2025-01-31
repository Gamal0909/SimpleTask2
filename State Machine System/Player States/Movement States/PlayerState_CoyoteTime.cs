using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState_CoyoteTime", menuName = "Data/StateMachine/PlayerStates/CoyoteTime")]
public class PlayerState_CoyoteTime : PlayerState
{
    [SerializeField] float _RunSpeed = 20f;
    [SerializeField] float _CoyoteTime = 0.1f;
    public override void Enter()
    {
        Debug.Log("Coyote Time");
        base.Enter();
        _PlayerController.SetUseGravity(false);
    }

    public override void Exit()
    {
        _PlayerController.SetUseGravity(true);
    }

    public override void LogicUpdate()
    {
        if (_PlayerInput.Jump)
        {
            _StateMachine.SwitchState(typeof(PlayerState_Jump));
        }

        if (_PlayerInput.Dash)
        {
            _StateMachine.SwitchState(typeof(PlayerState_Dash));
        }

        if (_StateDuration > _CoyoteTime || !_PlayerInput.Move)
        {
            _StateMachine.SwitchState(typeof(PlayerState_Fall));
        }
    }

    public override void PhysicsUpdate()
    {
        _PlayerController.Move(_RunSpeed);
    }
}

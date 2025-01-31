using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState_AttackIdle", menuName = "Data/StateMachine/PlayerStates/AttackIdle")]
public class PlayerState_AttackIdle : PlayerState
{
    private float _cooldownTime = 2.0f; // Cooldown duration in seconds
    private float _cooldownTimer;
    public override void Enter()
    {
        Debug.Log("Attack Idle");
        base.Enter();
        _cooldownTimer = _cooldownTime; // Reset the cooldown timer
    }

    public override void LogicUpdate()
    {
        if (_PlayerInput.StopJump || _PlayerController._IsFalling)
        {
            _StateMachine.SwitchState(typeof(PlayerState_Fall));
        }

        if (_PlayerInput.Attack)
        {
            _StateMachine.SwitchState(typeof(PlayerState_Attacking));
        }
        if (_PlayerInput.Move)
        {
            _StateMachine.SwitchState(typeof(PlayerState_AttackModeExit));
        }
        else if (_PlayerInput.Run)
        {
            _StateMachine.SwitchState(typeof(PlayerState_AttackModeExit));
        }
        else
        {
            _cooldownTimer -= Time.deltaTime; // Decrease the cooldown timer
            if (_cooldownTimer <= 0)
            {
                _StateMachine.SwitchState(typeof(PlayerState_AttackModeExit));
            }
        }
    }

    public override void PhysicsUpdate()
    {
    }
}


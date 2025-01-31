using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState_AttackModeExit", menuName = "Data/StateMachine/PlayerStates/AttackModeExit")]
public class PlayerState_AttackModeExit : PlayerState
{
    
    public override void Enter()
    {
        Debug.Log("Attack Mode Exit");
        base.Enter();
    }

    public override void LogicUpdate()
    {
        _StateMachine.SwitchState(typeof(PlayerState_Idle));
        
    }

    public override void PhysicsUpdate()
    {
    }
}

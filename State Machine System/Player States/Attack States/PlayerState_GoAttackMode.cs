using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState_GoAttackMode", menuName = "Data/StateMachine/PlayerStates/GoAttackMode")]
public class PlayerState_GoAttackMode : PlayerState
{
    public override void Enter()
    {
        Debug.Log("GoAttackMode");
        base.Enter();
        _CurrentSpeed = 0f;
    }

    public override void LogicUpdate()
    {
        //Move To Attack Idle State
        _StateMachine.SwitchState(typeof(PlayerState_AttackIdle));
    }

    public override void PhysicsUpdate()
    {
    }
}

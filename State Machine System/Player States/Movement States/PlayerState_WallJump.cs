using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState_WallJump", menuName = "Data/StateMachine/PlayerStates/WallJump")]
public class PlayerState_WallJump : PlayerState
{
    [SerializeField] float _WallJumpForce = 15f;
    [SerializeField] float _WallJumpUpForce = 10f;
    [SerializeField] float _WallJumpTime = 0.1f;

    private float _WallJumpStartTime;
    public override void Enter()
    {
        base.Enter();
        _WallJumpStartTime = Time.time;
    }

    public override void LogicUpdate()
    {
        if (_PlayerController._IsGrounded)
        {
            _StateMachine.SwitchState(typeof(PlayerState_Idle));
        }
    }   


    public override void PhysicsUpdate()
    {
        if (ShouldEndWallJump() == false)
        {
            _PlayerController.WallJump(_WallJumpForce,_WallJumpUpForce);
        }

        if (ShouldEndWallJump())
        {
            _StateMachine.SwitchState(typeof(PlayerState_Fall));
        }
        
    }

    private bool ShouldEndWallJump()
    {
        return Time.time - _WallJumpStartTime >= _WallJumpTime;
    }
}

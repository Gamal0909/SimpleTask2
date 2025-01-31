using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState_OnWall", menuName = "Data/StateMachine/PlayerStates/OnWall")]
public class PlayerState_OnWall : PlayerState
{

    [SerializeField] float _WaitTimeToSlide = 0.3f;
    [SerializeField] float _WallSlideSpeed = -20f;
    [SerializeField] float _WaitTimeToJump = 0.4f;

    private float _WaitToSlideStartTime;
    private float _WaitTimeToJumpStartTime;
    public override void Enter()
    {
        Debug.Log("WallIdle");
        base.Enter();
        _WaitToSlideStartTime = Time.time;
        _WaitTimeToJumpStartTime = Time.time;
    }

    public override void LogicUpdate()
    {
        if (_PlayerInput.Jump && ShouldJump())
        {
            _StateMachine.SwitchState(typeof(PlayerState_WallJump));    
        }
    }

    public override void PhysicsUpdate()
    {
        if(ShouldSlide() == false)
        {
            _PlayerController.SetVelocityOnY(_WallSlideSpeed);
        }
        
        if (ShouldSlide())
        {

            _StateMachine.SwitchState(typeof(PlayerState_WallSlide));
        }
    }

    private bool ShouldSlide()
    {
        return Time.time - _WaitToSlideStartTime >= _WaitTimeToSlide;
    }
    private bool ShouldJump()
    {
        return Time.time - _WaitTimeToJumpStartTime >= _WaitTimeToJump;
    }

    
}

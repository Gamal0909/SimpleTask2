using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayeState_Fall", menuName = "Data/StateMachine/PlayerStates/Fall")]
public class PlayerState_Fall : PlayerState
{
    [SerializeField] AnimationCurve speedCurve;
    [SerializeField] float _FallMoveSpeed = 9f;
    public override void LogicUpdate()
    {
        if(_PlayerInput.Attack){
            _StateMachine.SwitchState(typeof(PlayerState_AttackMidAir));
        }
        // if(_PlayerInput.HoldAttack){
        //     _StateMachine.SwitchState(typeof(PlayerState_FallAttack));
        // } 
        if (_PlayerInput.Dash)
        {
            if (_PlayerController._CanDash)
            {
                _StateMachine.SwitchState(typeof(PlayerState_Dash));
            }

        }
        if (_PlayerController._IsHangingOnLedge && !_PlayerController._WallCheck.IsWalled && _PlayerController._CanHoldOnLedge)
        {
            _StateMachine.SwitchState(typeof(PlayerState_LedgeGrab));
        }
        if (_PlayerController._IsGrounded)
        {
            _StateMachine.SwitchState(typeof(PlayerState_Land));
        }
        //if at wall go to on wall state
        if (_PlayerController._WallCheck.IsWalled && !_PlayerController._IsGrounded)
        {
            _StateMachine.SwitchState(typeof(PlayerState_OnWall));
        }
        if (_PlayerInput.Jump)
        {
            //Air Jump
            if(_PlayerController._CanAirJump)
            {
                _StateMachine.SwitchState(typeof(PlayerState_AirJump));

                return;
            }

            _PlayerInput.HasJumpInputBuffer = true;
        }
        
    }

    public override void PhysicsUpdate()
    {

        if (_PlayerController._WallCheck.IsWalled)
        {
            _PlayerController.Move(0);
        }
        else
        {
            _PlayerController.Move(_FallMoveSpeed);
        }
        _PlayerController.SetVelocityOnY(speedCurve.Evaluate(_StateDuration));

    }
}

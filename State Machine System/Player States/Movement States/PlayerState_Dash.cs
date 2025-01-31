using Cinemachine;
using System;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayeState_Dash", menuName = "Data/StateMachine/PlayerStates/Dash")]
public class PlayerState_Dash : PlayerState
{
    [SerializeField] float _DashPower = 10f;
    [SerializeField] float _StiffTime = 0.2f;

    public override void Enter()
    {
        base.Enter();
        _PlayerController.Dash(_DashPower);
        if (_PlayerController._IsDashing == true)
        {
            _PlayerController._DashEffect.active = true;
        }
    }
    public override void Exit()
    {
        base.Exit();
        _PlayerController._DashEffect.active = false;
    }
    public override void LogicUpdate()
    {
        if (_StateDuration < _StiffTime)
        {
            return;
        }

        if (_PlayerInput.Jump)
        {
  
            _StateMachine.SwitchState(typeof(PlayerState_Jump));
        }

        if (_PlayerInput.Move)
        {

            _StateMachine.SwitchState(typeof(PlayerState_Walk));
        }

        //if at wall go to on wall state
        if (_PlayerController._WallCheck.IsWalled && !_PlayerController._IsGrounded)
        {
            _StateMachine.SwitchState(typeof(PlayerState_OnWall));
        }

        else
        {

            _StateMachine.SwitchState(typeof(PlayerState_Idle));
        }

    }
}

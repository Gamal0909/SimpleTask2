using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayeState_Land", menuName = "Data/StateMachine/PlayerStates/Land")]
public class PlayerState_Land : PlayerState
{
    [SerializeField] float _StiffTime = 0.15f;
    public override void Enter()
    {
        Debug.Log("Land");
        base.Enter();
        _PlayerController.SetVelocity(Vector3.zero);
        _PlayerController._CanAirJump = true;
    }
    public override void LogicUpdate()
    {
        if (_PlayerInput.HasJumpInputBuffer||_PlayerInput.Jump)
        {
            _StateMachine.SwitchState(typeof(PlayerState_Jump));
        }

        if(_StateDuration < _StiffTime)
        {
            return;
        }

        if (_PlayerInput.Move)
        {
            _StateMachine.SwitchState(typeof(PlayerState_Walk));
        }
        if (_IsAnimationFinished)
        {
            _StateMachine.SwitchState(typeof(PlayerState_Idle));
        }
    }


}

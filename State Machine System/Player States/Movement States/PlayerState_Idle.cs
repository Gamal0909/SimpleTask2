using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "PlayeState_Idle", menuName = "Data/StateMachine/PlayerStates/Idle")]
public class PlayerState_Idle : PlayerState
{
   public override void Enter()
   {
        Debug.Log("Idle");
       base.Enter();
        _CurrentSpeed = _PlayerController._MoveSpeed;
   }

    public override void LogicUpdate()
    {
        if(_PlayerInput.Attack)
        {
            _StateMachine.SwitchState(typeof(PlayerState_Attacking));
        }
        if (_PlayerInput.Move)
        {
           _StateMachine.SwitchState(typeof(PlayerState_Walk));
        }

        if (_PlayerInput.Jump)
        {
            _StateMachine.SwitchState(typeof(PlayerState_Jump));
        }

        if (_PlayerInput.Dash)
        {
            if (_PlayerController._CanDash)
            {
                _StateMachine.SwitchState(typeof(PlayerState_Dash));
            }
            
        }

        if (!_PlayerController._IsGrounded)
        {
            _StateMachine.SwitchState(typeof(PlayerState_Fall));
        }

        

        _CurrentSpeed = Mathf.MoveTowards(_CurrentSpeed, 0, 10 * Time.deltaTime);
    }

    public override void PhysicsUpdate()
    {
       _PlayerController.Move(_CurrentSpeed);
    }
}

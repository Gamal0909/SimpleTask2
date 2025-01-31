using UnityEngine;

[CreateAssetMenu(fileName = "PlayeState_Run", menuName = "Data/StateMachine/PlayerStates/Run")]
public class PlayerState_Run : PlayerState
{
    [SerializeField] float _RunSpeed = 20f;
    [SerializeField] float _Acceleration = 10f;
   public override void Enter()
   {
        Debug.Log("Run");
       base.Enter();

        _CurrentSpeed = _PlayerController._MoveSpeed;
   }

    public override void LogicUpdate()
    {
        if (_PlayerInput.Attack)
        {
            _StateMachine.SwitchState(typeof(PlayerState_Attacking));
        }
        if (!_PlayerInput.Move)
        {
           _StateMachine.SwitchState(typeof(PlayerState_Idle));
        }

        else if (_PlayerInput.StopRun)
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

        if(!_PlayerController._IsGrounded)
        {
            _StateMachine.SwitchState(typeof(PlayerState_CoyoteTime));
        }

        _CurrentSpeed = Mathf.MoveTowards(_CurrentSpeed, _RunSpeed, _Acceleration * Time.deltaTime);
    }

    public override void PhysicsUpdate()
    {
        _PlayerController.Move(_CurrentSpeed);
    }


}

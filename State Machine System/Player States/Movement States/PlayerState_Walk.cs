using UnityEngine;

[CreateAssetMenu(fileName = "PlayeState_Walk", menuName = "Data/StateMachine/PlayerStates/Walk")]
public class PlayerState_Walk : PlayerState
{
    [SerializeField]float _WalkSpeed = 10f;
    [SerializeField]float _Acceleration = 5f;
    public override void Enter()
    {
        Debug.Log("Walk");
        base.Enter();
        _CurrentSpeed = 0f;
    }

    public override void LogicUpdate()
    {
        if (_PlayerInput.Attack)
        {
            _StateMachine.SwitchState(typeof(PlayerState_Attacking));
        }
        if ((_PlayerInput.Move)&&_PlayerInput.Run)
        {
            _StateMachine.SwitchState(typeof(PlayerState_Run));
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
            _StateMachine.SwitchState(typeof(PlayerState_CoyoteTime));
        }

        if (!_PlayerInput.Move)
        {
            _StateMachine.SwitchState(typeof(PlayerState_Idle));
        }

        _CurrentSpeed = Mathf.MoveTowards(_CurrentSpeed, _WalkSpeed, _Acceleration * Time.deltaTime);
    }

    public override void PhysicsUpdate()
    {
        _PlayerController.Move(_CurrentSpeed);
    }
}

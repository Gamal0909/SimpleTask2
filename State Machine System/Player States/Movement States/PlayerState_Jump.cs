using UnityEngine;

[CreateAssetMenu(fileName = "PlayeState_Jump", menuName = "Data/StateMachine/PlayerStates/Jump")]
public class PlayerState_Jump : PlayerState
{
    [SerializeField] float _JumpForce = 10f;
    [SerializeField] float _SpeedInAir = 5f;
    [SerializeField] float _JumpCancelTime = 0.4f;


    private float _JumpStartTime;
    public override void Enter()
    {
        Debug.Log("Jump");
        base.Enter();
        _PlayerController.SetVelocityOnY(_JumpForce);
        _PlayerInput.HasJumpInputBuffer = false;
        _JumpStartTime = Time.time;
    }
    public override void LogicUpdate()
    {
        if (_PlayerInput.Attack)
        {
            _StateMachine.SwitchState(typeof(PlayerState_AttackMidAir));
        }
        // if (_PlayerInput.HoldAttack)
        // {
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
        //if at wall go to on wall state
        if (_PlayerController._WallCheck.IsWalled && !_PlayerController._IsGrounded)
        {
            _StateMachine.SwitchState(typeof(PlayerState_OnWall));

        }

        if (_PlayerInput.StopJump || _PlayerController._IsFalling || ShouldCancelJump())
        {
            CancelJump();
            _StateMachine.SwitchState(typeof(PlayerState_Fall));
            return;
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
            _PlayerController.Move(_SpeedInAir);
        }

    }
    private bool ShouldCancelJump()
    {
        return Time.time - _JumpStartTime >= _JumpCancelTime;
    }

    private void CancelJump()
    {
        _StateMachine.SwitchState(typeof(PlayerState_Fall));
    }
}

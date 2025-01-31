using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState_WallSlide", menuName = "Data/StateMachine/PlayerStates/WallSlide")]
public class PlayerState_WallSlide : PlayerState
{
    [SerializeField] float _WallSlideSpeed = -2f;

    public override void Enter()
    {
        base.Enter();
        _PlayerController.SetVelocity(new Vector3(0,-1,0));

    }

    public override void LogicUpdate()
    {

        if (_PlayerInput.Jump)
        {
            _StateMachine.SwitchState(typeof(PlayerState_WallJump));    
        }
       
        if(!_PlayerController._WallCheck.IsWalled)
        {
            _StateMachine.SwitchState(typeof(PlayerState_Fall));
        }
        if(_PlayerController._IsGrounded)
        {
            _StateMachine.SwitchState(typeof(PlayerState_Idle));
        }

    }

    public override void PhysicsUpdate()
    {
        if (_PlayerController._WallCheck.IsWalled)
        {
            _PlayerController.SetVelocityOnY(_WallSlideSpeed);
        }
    }
}

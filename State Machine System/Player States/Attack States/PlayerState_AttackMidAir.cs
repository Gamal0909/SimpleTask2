using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState_AttackMidAir", menuName = "Data/StateMachine/PlayerStates/AttackMidAir")]
public class PlayerState_AttackMidAir : PlayerState
{
    [SerializeField] private float _comboResetTime = 0.5f;
    [SerializeField] private float _attackInputBufferTime = 0.2f;

    private int _currentAttackIndex = 0;
    private float _lastAttackTime;
    private float _lastInputTime;
    private bool _isComboInProgress = false;
    private bool _nextAttackQueued = false;
    private GameObject _currentSlashEffect;

    private static readonly int[] AttackHashes = new int[4]
    {
        Animator.StringToHash("Attack 1"),
        Animator.StringToHash("Attack 2"),
        Animator.StringToHash("Attack 3"),
        Animator.StringToHash("Attack H")
    };

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Attacking");
        _PlayerController.SetUseGravity(false);
        _PlayerController.SetVelocity(Vector3.zero);
        _currentAttackIndex = 0;
        _isComboInProgress = true;
        PerformAttack();
    }

    public override void LogicUpdate()
    {
        if (_PlayerInput.Attack && !_nextAttackQueued)
        {
            _nextAttackQueued = true;
            _lastInputTime = Time.time;
        }

        if (_IsAnimationFinished)
        {
            if (_nextAttackQueued)
            {
                PerformAttack();
            }
            else if (Time.time - _lastAttackTime >= _comboResetTime)
            {
                ExitAttackState();
            }
        }
        // Add this else block to prevent immediate attack execution
        else if (_nextAttackQueued && Time.time - _lastInputTime > _attackInputBufferTime)
        {
            _nextAttackQueued = false;
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        _PlayerController.SetVelocityOnY(-20);
    }

    private void PerformAttack()
    {
        _Animator.CrossFade(AttackHashes[_currentAttackIndex], TransitionDuration);
        _lastAttackTime = Time.time;
        _nextAttackQueued = false;

        // Use the new method in PlayerController
        _PlayerController.PerformAttack(_currentAttackIndex);

        _currentAttackIndex++;
        if (_currentAttackIndex >= AttackHashes.Length)
        {
            _currentAttackIndex = 0;
        }
        _isComboInProgress = true;
    }

    private void ExitAttackState()
    {
        _isComboInProgress = false;
        _StateMachine.SwitchState(typeof(PlayerState_AttackIdle));
    }

    public override void Exit()
    {
        base.Exit();
        _PlayerController.SetUseGravity(true);
        _PlayerController.SetVelocityOnY(-7);
        _nextAttackQueued = false;
        _PlayerController.ExitAttack();
    }
}

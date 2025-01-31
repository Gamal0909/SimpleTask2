using Cinemachine;
using UnityEngine;

public class PlayerState : ScriptableObject, IState
{
    [SerializeField] string _StateName;
    [SerializeField, Range(0f,1f)] float _TranstionDuration = 0.1f;

    float _StateStartTime;
    int _StateHash;
    protected float _CurrentSpeed;
    protected Animator _Animator;
    protected PlayerStateMachine _StateMachine;
    protected PlayerInput _PlayerInput;
    protected PlayerController _PlayerController;
    protected bool _IsAnimationFinished => _StateDuration >= _Animator.GetCurrentAnimatorStateInfo(0).length;
    protected float _StateDuration => Time.time - _StateStartTime;
    protected float TransitionDuration => _TranstionDuration;
    void OnEnable()
    {
        
        _StateHash = Animator.StringToHash(_StateName);
    }

    public void Initialize(Animator animator, PlayerInput playerInput,PlayerController playerController ,PlayerStateMachine playerStateMachine)
    {
        this._Animator = animator;
        this._PlayerInput = playerInput;
        this._PlayerController = playerController;
        this._StateMachine = playerStateMachine;
    }
    public virtual void Enter()
    {
        _Animator.CrossFade(_StateHash,_TranstionDuration);
        _StateStartTime = Time.time;
    }

    public virtual void Exit()
    {
        
    }

    public virtual void LogicUpdate()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        
    }
}

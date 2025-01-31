using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{

    [SerializeField] PlayerState[] playerStates;
    Animator _Animator;
    PlayerInput _PlayerInput;
    PlayerController _PlayerController;

    void Awake()
    {
        _Animator = GetComponent<Animator>();
        _PlayerInput = GetComponent<PlayerInput>();
        _PlayerController = GetComponent<PlayerController>();

        _StatesTable = new Dictionary<System.Type, IState>(playerStates.Length);

        foreach (PlayerState state in playerStates)
        {
            state.Initialize(_Animator,_PlayerInput,_PlayerController, this);
            _StatesTable.Add(state.GetType(), state);
        }
    }

    void Start()
    {
        // Set the initial state
        SwitchOn(_StatesTable[typeof(PlayerState_Idle)]);
    }
}

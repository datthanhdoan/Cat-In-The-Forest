using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum PlayerState
{
    Idle,
    Move

}
public class Player : GenericSingleton<Player>
{
    public static event Action<PlayerState> OnPlayerStateChanged;
    public PlayerState playerState { get; private set; } = PlayerState.Idle;
    PlayerState previousState;
    [NonSerialized] public NavMeshAgent agent;
    InputManager _inputManager;

    [SerializeField] Animator _anim;
    [SerializeField] float _speed = 5;

    void Start()
    {
        _inputManager = InputManager.Instance;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = _speed;
    }

    void Update()
    {
        previousState = playerState;

        // If player clicks on the screen 
        if (CheckClick())
        {
            // Move player to the clicked position
            DoMove();
            playerState = PlayerState.Move;
        }

        // If player is not moving
        bool notMove = playerState != PlayerState.Idle && agent.velocity == Vector3.zero;
        if (notMove)
        {
            playerState = PlayerState.Idle;
        }

        // Only invoke the event if the state has actually changed
        if (playerState != previousState)
        {
            OnPlayerStateChanged?.Invoke(playerState);
        }
    }

    public bool CheckClick()
    {
        return _inputManager.CheckClickInArea() && !_inputManager.CheckClickOnUI();
    }

    public void DoMove()
    {
        agent.SetDestination(_inputManager.clickPos);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
    public void PlayerSpeed(float newSpeed)
    {
        _speed = newSpeed;
        agent.speed = _speed;
    }

}

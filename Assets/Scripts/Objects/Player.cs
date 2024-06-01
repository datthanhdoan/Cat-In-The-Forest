using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
    private PlayerState previousState = PlayerState.Idle;
    [NonSerialized] public NavMeshAgent _agent;
    private InputManager _inputManager;

    [SerializeField] float _speed = 5;

    private Vector3 _previousPosition;
    private Vector3 _currentPosition;

    void Start()
    {
        _inputManager = InputManager.Instance;
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.speed = _speed;
        OnPlayerStateChanged?.Invoke(playerState);
    }

    private void FixedUpdate()
    {
        // If player clicks on the screen 
        if (_previousPosition != _currentPosition)
        {
            // Move player to the clicked position
            DoMove();
            _previousPosition = _currentPosition;
        }
        // If player is not moving

        UpdatePlayerState();

        // Only invoke the event if the state has actually changed
        DetectStateChange();
    }

    private void DetectStateChange()
    {
        if (playerState != previousState)
        {
            OnPlayerStateChanged?.Invoke(playerState);
            previousState = playerState;
            // Debug.Log("State changed to: " + playerState);
        }
    }

    public void UpdatePlayerState()
    {
        if (_agent.remainingDistance < 0.1f)
        {
            playerState = PlayerState.Idle;
        }
        else
        {
            playerState = PlayerState.Move;
        }
    }

    public void OnPlayerClickInMap(Vector3 clickPos)
    {
        _currentPosition = clickPos;
    }

    private void OnEnable()
    {
        InputManager.OnClick += OnPlayerClickInMap;
    }
    private void OnDisable()
    {
        InputManager.OnClick -= OnPlayerClickInMap;
    }

    public void DoMove()
    {
        _agent.SetDestination(_currentPosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
    public void PlayerSpeed(float newSpeed)
    {
        _speed = newSpeed;
        _agent.speed = _speed;
    }

}

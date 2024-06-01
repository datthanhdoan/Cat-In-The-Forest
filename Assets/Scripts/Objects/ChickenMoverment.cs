using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor.EventSystems;
using Random = UnityEngine.Random;

public class ChickenMoverment : Subject
{
    private NavMeshAgent _agent;
    private ChickenState _currentState = ChickenState.Idle;
    private ChickenState _previousState = ChickenState.Idle;
    public ChickenState CurrentState => _currentState;
    private Vector3 _targetPos;
    public Vector3 TargetPos => _targetPos;
    [SerializeField] private Pasture _pasture;


    public enum ChickenState
    {
        Idle,
        Walk,
        Run,
        Eat,
        Sleep
    }
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        var wait = 5;
        StartCoroutine(WaitAndWalk(wait));
    }

    private void Update()
    {
        if (_agent.velocity.magnitude > 0.4f)
        {
            _currentState = ChickenState.Walk;
        }
        else
        {
            _currentState = ChickenState.Idle;
        }
        if (_currentState != _previousState)
        {
            UpdateChichenState(_currentState);
        }
    }

    private void UpdateChichenState(ChickenState state)
    {
        _previousState = _currentState;
        _currentState = state;
        NotifyObservers();
    }



    private IEnumerator WaitAndWalk(float wait)
    {
        yield return new WaitForSeconds(wait);
        Walk();
    }

    private void Walk()
    {
        _targetPos = _pasture.RandomPositionInBounds();
        _agent.SetDestination(_targetPos);
        StartCoroutine(WaitAndWalk(Random.Range(10, 20)));
    }
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class ChickenMoverment : Subject
{
    private int _maxHunger = 100;
    private int _hungerLevel = 0;
    private int _eatThreshold = 30; // GO TO EAT
    private int _hungerLimit = 5; // Ăn vạ =))

    private NavMeshAgent _agent;
    private ChickenState _currentState = ChickenState.Idle;
    private ChickenState _previousState = ChickenState.Idle;

    public ChickenState CurrentState => _currentState;
    private Vector3 _targetPos;
    public Vector3 TargetPos => _targetPos;

    [SerializeField] private Pasture _pasture;
    [SerializeField] private Troung _trough;

    public enum ChickenState
    {
        Idle,
        Walk,
        Run,
        NeedToEat,
        Sleep
    } //

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        var wait = 5;
        StartCoroutine(WaitForWalk(wait));
        InvokeRepeating(nameof(CountHunger), 1, 1);
    }

    private void CountHunger()
    {
        _hungerLevel--;
        if (_hungerLevel <= _eatThreshold)
        {
            _currentState = ChickenState.NeedToEat;
        }
        else if (_hungerLevel <= _hungerLimit)
        {
            _currentState = ChickenState.Sleep;
        }
    }

    private void GotoEat()
    {
        var riceAmountTaken = 4;
        _targetPos = _trough.transform.position;
        _agent.SetDestination(_targetPos);

        if (_agent.remainingDistance < 0.4f)
        {
            _agent.ResetPath();
            _trough.TakeRice(riceAmountTaken);
            _hungerLevel = _maxHunger;
            _currentState = ChickenState.Idle;
            StartCoroutine(WaitForWalk(Random.Range(10, 20)));
        }

    }

    private void Update()
    {
        if (_agent.remainingDistance < 0.1f)
        {
            _agent.ResetPath();
            _currentState = ChickenState.Idle;
        }
        if (_currentState != _previousState)
        {
            UpdateChichenState(_currentState);
        }
        HandelStateChange(_currentState);
    }

    private void HandelStateChange(ChickenState state)
    {
        switch (state)
        {
            case ChickenState.Idle:
                break;
            case ChickenState.Walk:
                break;
            case ChickenState.Run:
                break;
            case ChickenState.NeedToEat:
                GotoEat();
                break;
            case ChickenState.Sleep:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
    private void UpdateChichenState(ChickenState state)
    {
        _previousState = _currentState;
        _currentState = state;
        NotifyObservers();
    }



    private IEnumerator WaitForWalk(float wait)
    {
        yield return new WaitForSeconds(wait);
        Walk();
    }

    private void Walk()
    {
        if (_currentState != ChickenState.NeedToEat)
        {
            _targetPos = _pasture.RandomPositionInBounds();
            _agent.SetDestination(_targetPos);
            _currentState = ChickenState.Walk;
            StartCoroutine(WaitForWalk(Random.Range(10, 20)));
        }

    }
}

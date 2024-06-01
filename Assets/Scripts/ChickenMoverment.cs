using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChickenMoverment : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] private Pasture _pasture;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        var wait = 5;
        StartCoroutine(WaitAndWalk(wait));
    }

    private IEnumerator WaitAndWalk(float wait)
    {
        yield return new WaitForSeconds(wait);
        Walk();
    }

    private void Walk()
    {
        Vector3 randomPosition = _pasture.RandomPositionInBounds();
        _agent.SetDestination(randomPosition);
        StartCoroutine(WaitAndWalk(Random.Range(10, 20)));
    }






}

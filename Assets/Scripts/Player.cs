using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameManagerment gameManagerment;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        gameManagerment = GameObject.Find("GameManager").GetComponent<GameManagerment>();
    }

    void Update()
    {
        if (gameManagerment.CheckClickInArea() && gameManagerment.clickPos.gameObject.activeSelf)
        {
            agent.SetDestination(gameManagerment.clickPos.position);
        }



    }
}

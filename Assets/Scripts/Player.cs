using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [NonSerialized] public NavMeshAgent agent;
    GameManagerment _gameManagerment;
    [SerializeField] Animator _anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        _gameManagerment = GameObject.Find("GameManager").GetComponent<GameManagerment>();
    }

    void Update()
    {
        if (_gameManagerment.CheckClickInArea() && !_gameManagerment.CheckClickOnUI())
        {
            agent.SetDestination(_gameManagerment.clickPos.position);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        PlayerAnim();
    }

    void PlayerAnim()
    {
        if (agent.velocity == Vector3.zero) _anim.CrossFade(Player_Idle, 0f);
        if (agent.velocity != Vector3.zero) _anim.CrossFade(Player_Move, 0f);
    }

    private static readonly int Player_Move = Animator.StringToHash("Player_Move");
    private static readonly int Player_Idle = Animator.StringToHash("Player_Nor");

}

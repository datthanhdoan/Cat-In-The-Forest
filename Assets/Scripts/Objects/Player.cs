using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : GenericSingleton<Player>
{
    [NonSerialized] public NavMeshAgent agent;
    GameManagerment _gm;

    [SerializeField] Animator _anim;
    [SerializeField] float _speed = 5;

    void Start()
    {
        _gm = GameManagerment.instance;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = _speed;
    }

    void Update()
    {
        if (_gm.CheckClickInArea() && !_gm.CheckClickOnUI())
        {
            agent.SetDestination(_gm.clickPos.position);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        PlayerAnim();
    }
    public void PlayerSpeed(float newSpeed)
    {
        _speed = newSpeed;
        agent.speed = _speed;
    }

    void PlayerAnim()
    {
        if (agent.velocity == Vector3.zero) _anim.CrossFade(Player_Idle, 0f);
        if (agent.velocity != Vector3.zero) _anim.CrossFade(Player_Move, 0f);
    }

    public bool CheckMoving()
    {
        return agent.velocity == Vector3.zero ? false : true;
    }

    private static readonly int Player_Move = Animator.StringToHash("Player_Move");
    private static readonly int Player_Idle = Animator.StringToHash("Player_Nor");

}

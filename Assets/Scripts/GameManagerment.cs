using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class GameManagerment : MonoBehaviour
{

    public Player playerScript;

    public Transform clickPos;
    // Inventory
    public int Coint = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) clickPos = GetMousePosition();
    }

    public bool CheckPlayerMoving()
    {
        return playerScript.agent.velocity == Vector3.zero ? false : true;
    }

    public Transform GetMousePosition()
    {
        Transform trans = clickPos;
        trans.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        trans.position = new Vector3(trans.position.x, trans.position.y, 0);
        return trans;
    }
    public bool CheckClickInArea()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(clickPos.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            clickPos.gameObject.SetActive(true);
            return true;
        }
        else
        {
            clickPos.gameObject.SetActive(false);
            return false;
        }
    }

    public bool CheckClickOnUI()
    {
        bool check = EventSystem.current.IsPointerOverGameObject();
        return check;
    }
}

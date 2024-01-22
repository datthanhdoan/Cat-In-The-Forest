using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class GameManagerment : MonoBehaviour
{

    public Player playerScript;
    public NavMeshSurface Surface2D;
    public Transform clickPos;
    // Inventory
    public GameObject region;
    private TaskManagerment _taskManagerment;
    [NonSerialized] public int currentLevel = 1;

    private void Start()
    {
        _taskManagerment = GameObject.Find("TaskManager").GetComponent<TaskManagerment>();
        Surface2D.BuildNavMeshAsync(); // Init NavMesh
        Surface2D = GameObject.Find("NavhMesh").GetComponent<NavMeshSurface>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) clickPos = GetMousePosition();
    }

    public void UpdateNavMesh()
    {
        Surface2D.UpdateNavMesh(Surface2D.navMeshData);
    }

    public bool CheckPlayerMoving()
    {
        return playerScript.agent.velocity == Vector3.zero ? false : true;
    }

    public void UpdateQuantityOfFruit(int quantity)
    {
        _taskManagerment.quantityOfFruit += quantity;
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

    public float CheckDistance(Vector2 x, Vector2 y)
    {
        return Vector2.Distance(x, y);
    }
}

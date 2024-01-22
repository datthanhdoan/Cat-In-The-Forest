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
    public Transform clickPos;
    public GameObject region;
    public int maxLevel; // equal region.childCount in Start()
    private TaskManagerment _taskManagerment;
    public int currentLevel { get; private set; } = 1;

    private NavMeshSurface Surface2D;
    private void Start()
    {
        maxLevel = region.transform.childCount;
        _taskManagerment = GameObject.Find("TaskManager").GetComponent<TaskManagerment>();
        Surface2D = GameObject.Find("NavhMesh").GetComponent<NavMeshSurface>();
        Surface2D.BuildNavMeshAsync(); // Init NavMesh
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) clickPos = GetMousePosition();
    }

    public void UpdateNavMesh()
    {
        Surface2D.UpdateNavMesh(Surface2D.navMeshData);
    }

    public bool MaxLevelCheck()
    {
        return currentLevel >= maxLevel ? true : false;
    }

    public void UpdateLevel()
    {
        currentLevel++;
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

using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
/// <summary>
/// Quản lý game
/// </summary>
public class GameManagerment : GenericSingleton<GameManagerment>
{
    public Transform clickPos;

    public bool CheckFirstTimeInGame()
    {
        if (PlayerPrefs.GetInt("FirstTimeInGame") == 1)
        {
            return true;
        }
        else
        {
            return true;
        }
    }



    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) clickPos = GetMousePosition();
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

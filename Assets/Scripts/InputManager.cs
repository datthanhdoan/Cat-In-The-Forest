using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class InputManager : GenericSingleton<InputManager>
{
    public Vector3 clickPos;
    public static event Action<Vector3> OnClick;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                clickPos = GetMousePosition();
                OnClick?.Invoke(clickPos);
            }
        }
    }

    public Vector3 GetMousePosition()
    {
        // Transform trans = clickPos;
        clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clickPos = new Vector3(clickPos.x, clickPos.y, 0);
        return clickPos;
    }
    public bool CheckClickInArea()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(clickPos, out hit, 1.0f, NavMesh.AllAreas))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckClickOnUI()
    {
        // bool check = EventSystem.current.IsPointerOverGameObject();
        // return check;
        // kiểm tra xem có click vào UI không
        return EventSystem.current.IsPointerOverGameObject();
    }
    public float CheckDistance(Vector2 x, Vector2 y)
    {
        return Vector2.Distance(x, y);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.EventSystems;

public class TaskManagerment : MonoBehaviour
{
    [SerializeField] private GameObject taskUI;
    private GameManagerment gameManagerment;
    [SerializeField] private bool hasBeenClicked = false;
    private void Start()
    {
        gameManagerment = GameObject.Find("GameManager").GetComponent<GameManagerment>();
    }
    void Update()
    {

        if (hasBeenClicked)
        {
            if (!gameManagerment.CheckPlayerMoving() && CheckDistance())
            {
                hasBeenClicked = false;
                taskUI.SetActive(CheckDistance() && !gameManagerment.CheckPlayerMoving());
            }
        }
    }
    public void OnMouseDown()
    {
        hasBeenClicked = true;
        Debug.Log("Click");
    }
    bool CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, GameObject.Find("Player").transform.position);
        if (distance < 1.75f)
        {
            return true;
        }
        return false;
    }
}

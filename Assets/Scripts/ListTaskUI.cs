using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListTaskUI : MonoBehaviour
{
    TaskManagerment _taskManagerment;
    // Start is called before the first frame update
    void Start()
    {
        _taskManagerment = GameObject.Find("TaskManager").GetComponent<TaskManagerment>();
    }

    private void OnEnable()
    {
        _taskManagerment.ListTaskUI(gameObject);
    }
    // Update is called once per frame
    public void CheckTask()
    {
        _taskManagerment.CheckTask();
    }
}

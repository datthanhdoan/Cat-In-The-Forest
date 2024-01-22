using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.EventSystems;

public class TaskManagerment : MonoBehaviour
{
    //public 
    [SerializeField] private GameObject taskUI;
    [SerializeField] private GameObject completeTaskUI;
    [SerializeField] public int quantityOfFruitRequire = 0;
    // create quantityOfFruit variable private set public get
    [SerializeField] public int quantityOfFruit = 0;
    public int coint;

    //private
    GameManagerment _gm;
    [SerializeField] bool _hasBeenClicked = false;
    [SerializeField] GameObject _listTask;
    private void Start()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManagerment>();
        UpdateFruitRequire();
    }
    void Update()
    {

        if (_hasBeenClicked)
        {
            if (!_gm.CheckPlayerMoving() && CheckDistance() <= 1.5f)
            {
                _hasBeenClicked = false;
                taskUI.SetActive(true);
                ListTaskUI();
            }
        }
    }

    #region MathRecipe

    public void ListTaskUI()
    {
        GameObject task = _listTask.transform.GetChild(0).gameObject;
        Text taskText = task.transform.GetChild(1).GetComponent<Text>();
        if (_gm.MaxLevelCheck())
        {
            // hide all task
            foreach (Transform child in _listTask.transform)
            {
                child.gameObject.SetActive(false);
            }

            // show complete message
            string completeMessage = "You have completed all tasks";
            completeTaskUI.SetActive(true);
            completeTaskUI.GetComponent<Text>().text = completeMessage;

        }
        if (!_gm.MaxLevelCheck())
        {
            completeTaskUI.SetActive(false);
            string taskMessage = "UnlokLevel " + (_gm.currentLevel + 1) + "Need : " + quantityOfFruitRequire + " Fruit";
            taskText.text = taskMessage;
        }
    }
    public void UpdateFruitRequire()
    {
        quantityOfFruitRequire += (int)(_gm.currentLevel * Random.Range(2.5f, 5.5f));
    }

    public void CheckTask()
    {
        if (quantityOfFruit >= quantityOfFruitRequire)
        {
            if (!_gm.MaxLevelCheck())
            {
                _gm.UpdateLevel(); // Update new level
                UpdateFruitRequire(); // Update new task

                // remove bound of old level
                GameObject boundOfPreviousLevel = _gm.region.transform.GetChild(_gm.currentLevel - 2).gameObject;
                boundOfPreviousLevel.transform.GetChild(1).gameObject.SetActive(false);

                // update UI
                ListTaskUI();

                // update navmesh
                _gm.UpdateNavMesh();
            }
        }
        else
        {
            Debug.Log("Not enough fruit");
        }
    }

    #endregion

    #region CheckDistance
    public void OnMouseUpAsButton()
    {
        _hasBeenClicked = true;
        Debug.Log("Click");
    }
    float CheckDistance()
    {
        float distance = _gm.CheckDistance(transform.position, _gm.playerScript.transform.position);
        return distance;
    }

    #endregion
}

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
    public int coin;

    //private
    GameManagerment _gm;
    bool _hasBeenClicked = false;
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
        GameObject unlockLV = _listTask.transform.GetChild(0).gameObject;
        Text unlockLV_Text = unlockLV.transform.GetChild(1).GetComponent<Text>();
        if (_gm.MaxLevelCheck())
        {

            // show max level message
            string maxLevelMessage = "You have unlocked all levels";
            unlockLV_Text.text = maxLevelMessage;
            // hide button in unlockLV
            unlockLV.transform.GetChild(2).gameObject.SetActive(false);

            // this code use when player complete all task
            // hide all task
            // foreach (Transform child in _listTask.transform)
            // {
            //     child.gameObject.SetActive(false);
            // }
            // string completeMessage = "You have completed all tasks";
            // completeTaskUI.SetActive(true);
            // completeTaskUI.GetComponent<Text>().text = completeMessage;

        }
        if (!_gm.MaxLevelCheck())
        {
            completeTaskUI.SetActive(false);
            string taskMessage = "Unlock level " + (_gm.currentLevel + 1) + " need : " + quantityOfFruitRequire + " fruit";
            unlockLV_Text.text = taskMessage;
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

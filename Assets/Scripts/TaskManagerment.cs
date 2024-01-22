using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.EventSystems;

public class TaskManagerment : MonoBehaviour
{
    [SerializeField] private GameObject taskUI;
    [SerializeField] public int quantityOfFruitRequire = 0;
    // create quantityOfFruit variable private set public get
    [SerializeField] public int quantityOfFruit = 0;
    public int coint;
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
        task.transform.GetChild(1).GetComponent<Text>().text = "UnlokLevel " + (_gm.currentLevel + 1) + "Need : " + quantityOfFruitRequire + " Fruit";
    }
    public void UpdateFruitRequire()
    {
        quantityOfFruitRequire += (int)(_gm.currentLevel * Random.Range(2.5f, 5.5f));
    }

    public void CheckTask()
    {
        if (quantityOfFruit >= quantityOfFruitRequire)
        {
            _gm.currentLevel++;
            UpdateFruitRequire(); // Update new task
            // remove bound of old level
            GameObject childI = _gm.region.transform.GetChild(_gm.currentLevel - 2).gameObject;
            childI.transform.GetChild(1).gameObject.SetActive(false);
            // open new level
            // _gm.region.transform.GetChild(_gm.currentLevel).gameObject.SetActive(true);
            _gm.UpdateNavMesh();
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

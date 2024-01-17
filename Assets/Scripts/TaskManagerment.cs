using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.EventSystems;

public class TaskManagerment : MonoBehaviour
{
    [SerializeField] private GameObject taskUI;
    GameManagerment _gm;
    public NavMeshSurface Surface2D;
    [SerializeField] bool _hasBeenClicked = false;
    int _quantityOfFruitRequire = 0;
    private void Start()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManagerment>();
    }
    void Update()
    {

        if (_hasBeenClicked)
        {
            if (!_gm.CheckPlayerMoving() && CheckDistance() <= 1.5f)
            {
                _hasBeenClicked = false;
                taskUI.SetActive(true);
            }
        }
    }

    #region MathRecipe

    public void ListTaskUI(GameObject list)
    {
        CreateTask();
        GameObject task = list.transform.GetChild(0).gameObject;
        task.transform.GetChild(1).GetComponent<Text>().text = "UnlokLevel " + (_gm.currentLevel + 1) + " : " + _quantityOfFruitRequire + " Fruit";
    }
    public void CreateTask()
    {
        _gm.quantityOfFruit += (int)(_gm.currentLevel * Random.Range(2.5f, 5.5f));
    }

    public void CheckTask()
    {
        if (_gm.quantityOfFruit >= _quantityOfFruitRequire)
        {
            _gm.currentLevel++;
            _gm.coint += _gm.currentLevel * 10;
            // remove bound of old level
            GameObject childI = _gm.region.transform.GetChild(_gm.currentLevel - 1).gameObject;
            childI.transform.GetChild(1).gameObject.SetActive(false);
            // open new level
            _gm.region.transform.GetChild(_gm.currentLevel).gameObject.SetActive(true);
            Surface2D.UpdateNavMesh(Surface2D.navMeshData);
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

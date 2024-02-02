using UnityEngine;

public class TaskManagerment : MonoBehaviour
{
    [SerializeField] GameObject taskUI;
    [SerializeField] GameObject _listTask;
    [SerializeField] GameObject _taskPrefab;
    [SerializeField] GameObject completeTaskUI;


    // tasks
    GameManagerment _gm;
    bool _hasBeenClicked = false;
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

    // public void UpdateTaskContent()
    // {
    //     Text _text = __taskCheckLevel.transform.GetChild(1).GetComponent<Text>();

    //     if (!_gm.MaxLevelCheck())
    //     {
    //         completeTaskUI.SetActive(false);
    //         string taskMessage = "Unlock level " + (_gm.currentLevel + 1) + " need : " + _coinRequire + " coin";
    //         _text.text = taskMessage;
    //     }

    //     if (_gm.MaxLevelCheck())
    //     {
    //         // show max level message
    //         string maxLevelMessage = "You have unlocked all levels";
    //         _text.text = maxLevelMessage;
    //         // hide button in __taskCheckLevel
    //         __taskCheckLevel.transform.GetChild(2).gameObject.SetActive(false);
    //     }
    // }

    // public void UpdateCoinRequire() => _coinRequire += (int)(_gm.currentLevel * Random.Range(2.5f, 5.5f));

    // public void UpdateFruitRequire() => quantityOfFruitRequire += (int)(_gm.currentLevel * Random.Range(2.5f, 5.5f));

    // void CheckFruitTask()
    // {
    //     if (coin >= _coinRequire)
    //     {
    //         if (!_gm.MaxLevelCheck())
    //         {
    //             _gm.UpdateLevel(); // Update new level
    //             UpdateFruitRequire();
    //             UpdateCoinRequire();

    //             // update navmesh -- map
    //             _gm.UpdateNavMesh();
    //         }
    //     }
    // }

    // void UpdateRegion()
    // {
    //     // remove bound of old level
    //     GameObject boundOfPreviousLevel = _gm.region.transform.GetChild(_gm.currentLevel - 2).gameObject;
    //     boundOfPreviousLevel.transform.GetChild(1).gameObject.SetActive(false);

    //     // update content of task
    //     UpdateTaskContent();
    // }

    // GameObject SpawnTask(string t1, Transform parent)
    // {
    //     GameObject task = Instantiate(_taskPrefab, parent);
    //     task.transform.GetChild(1).GetComponent<Text>().text = t1;
    //     task.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(CheckFruitTask);
    //     return task;
    // }

    #endregion

    #region CheckDistance
    public void OnMouseUpAsButton() => _hasBeenClicked = true;
    float CheckDistance() => Vector2.Distance(transform.position, _gm.playerScript.transform.position);

    #endregion
}

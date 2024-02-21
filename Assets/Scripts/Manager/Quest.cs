using UnityEngine;

public class Quest : MonoBehaviour
{

    [SerializeField] GameObject taskUI;
    [SerializeField] GameObject _listTask;
    [SerializeField] GameObject _taskPrefab;
    [SerializeField] GameObject completeTaskUI;


    // tasks
    Player _player;
    GameManagerment _gm;
    bool _hasBeenClicked = false;
    private void Start()
    {
        _player = Player.instance;
        _gm = GameManagerment.instance;
    }

    void Update()
    {

        if (_hasBeenClicked)
        {
            if (!_player.CheckMoving() && CheckDistance() <= 1.5f)
            {
                _hasBeenClicked = false;
                taskUI.SetActive(true);
            }
        }
    }

    #region CheckDistance
    public void OnMouseUpAsButton() => _hasBeenClicked = true;
    float CheckDistance() => Vector2.Distance(transform.position, _player.transform.position);

    #endregion
}

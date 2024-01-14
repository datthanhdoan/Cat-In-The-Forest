using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TreeScript : MonoBehaviour
{
    GameManagerment _gameManagerment;
    bool _hasBeenClicked = false;
    bool _hasFruit = false;
    float _timeToSpawn = 5f;
    float _timeToSpawnTimer = 0f;
    [SerializeField] GameObject _sprite;
    [SerializeField] GameObject _fruit;

    void Start()
    {
        _gameManagerment = GameObject.Find("GameManager").GetComponent<GameManagerment>();
        _fruit.SetActive(true);
        _hasFruit = _fruit.activeSelf;
    }
    void Update()
    {
        // Player dung lai va nguoi choi khong an chuot thi moi cho an
        if (_hasBeenClicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _hasBeenClicked = false;
            }
            else if (!_gameManagerment.CheckPlayerMoving())
            {
                _hasBeenClicked = false;
                TakeFruit();
                // TreeStretchAnim();
            }
        }
        _timeToSpawnTimer = _hasFruit ? 0f : _timeToSpawnTimer + Time.deltaTime;
        if (_timeToSpawnTimer >= _timeToSpawn)
        {
            SpawnFruit();
        }

    }
    public void OnMouseDown()
    {
        // TreeStretchAnim();
        _hasBeenClicked = true;
        Debug.Log("Click");
        // return true;
    }

    public void TakeFruit()
    {
        if (_hasFruit)
        {
            _hasFruit = false;
            _fruit.SetActive(false);
            _timeToSpawnTimer = 0f;
            _gameManagerment.Coint++;
        }
    }

    public void SpawnFruit()
    {
        if (!_hasFruit)
        {
            // TreeStretchAnim();
            _hasFruit = true;
            _fruit.SetActive(true);
        }
    }


    // private void TreeStretchAnim()
    // {

    //     _sprite.GetComponent<Animator>().CrossFade(TreeStretch, 0f);
    // }
    // private static readonly int TreeStretch = Animator.StringToHash("Tree-Stretch");

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class TreeScript : MonoBehaviour
{
    [SerializeField] Sprite[] _sprite;
    [SerializeField] GameObject _treeSprite;
    bool _hasBeenClicked = false;
    bool _hasFruit = false;
    float _timeToSpawn = 5f;
    float _timeToSpawnTimer = 0f;
    GameManagerment _gm;

    void Start()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManagerment>();
        _hasFruit = true;
        _treeSprite.GetComponent<SpriteRenderer>().sprite = _sprite[1];
    }
    void Update()
    {
        if (_hasBeenClicked)
        {
            // Check if player click to other place do not take fruit
            if (Input.GetMouseButtonDown(0))
            {
                _hasBeenClicked = false;
            }
            else if (!_gm.CheckPlayerMoving() && DistanceToPlayer() <= 1.5f)
            {
                _hasBeenClicked = false;
                TakeFruit();
                TreeStretch_Anim();
            }
        }
        _timeToSpawnTimer = _hasFruit ? 0f : _timeToSpawnTimer + Time.deltaTime;
        if (_timeToSpawnTimer >= _timeToSpawn)
        {
            SpawnFruit();
        }

    }

    public float DistanceToPlayer()
    {
        float distance = Vector2.Distance(_gm.playerScript.transform.position, transform.position);
        Debug.Log(distance);
        return distance;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
    public void OnMouseUpAsButton()
    {
        TreeStretch_Anim();
        _hasBeenClicked = true;
        Debug.Log("Click");
    }

    public void TakeFruit()
    {
        if (_hasFruit)
        {
            _hasFruit = false;
            _treeSprite.GetComponent<SpriteRenderer>().sprite = _sprite[0];
            _timeToSpawnTimer = 0f;
            _gm.UpdateQuantityOfFruit(1);
        }
    }

    public void SpawnFruit()
    {
        if (!_hasFruit)
        {
            TreeStretch_Anim();
            _hasFruit = true;
            _treeSprite.GetComponent<SpriteRenderer>().sprite = _sprite[1];
        }
    }


    private void TreeStretch_Anim()
    {
        _treeSprite.GetComponent<Animator>().CrossFade(TreeStretch, 0f);
    }
    private static readonly int TreeStretch = Animator.StringToHash("Tree-Stretch");

}

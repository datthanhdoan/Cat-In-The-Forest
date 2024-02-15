using Unity.VisualScripting;
using UnityEngine;


public class TreeScript : MonoBehaviour
{
    [SerializeField] Sprite[] _sprite;
    [SerializeField] SpriteRenderer _treeSprite;
    [SerializeField] Animator _treeAnim;

    bool _hasBeenClicked = false;
    bool _hasFruit = false;
    float _timeToSpawn = 8f;
    float _timeToSpawnTimer = 0f;

    ProgressManager _pm;
    GameManagerment _gm;
    Player _player;
    void Start()
    {
        _pm = ProgressManager.instance;
        _gm = GameManagerment.instance;
        _player = Player.instance;
        _hasFruit = true;
        _treeSprite.sprite = _sprite[1];
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
            else if (!_player.CheckMoving() && DistanceToPlayer() <= 1.5f)
            {
                _hasBeenClicked = false;
                TakeFruitAnim();
                _pm.UpdateApple(1);
                TreeStretch_Anim();
            }
        }

        // Timer to spawn fruit
        _timeToSpawnTimer = _hasFruit ? 0f : _timeToSpawnTimer + Time.deltaTime;
        if (_timeToSpawnTimer >= _timeToSpawn)
        {
            SpawnFruit();
        }

    }

    public float DistanceToPlayer()
    {
        float distance = Vector2.Distance(_player.transform.position, transform.position);
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
    }

    public void TakeFruitAnim()
    {
        if (_hasFruit)
        {
            _hasFruit = false;
            _treeSprite.sprite = _sprite[0];
            _timeToSpawnTimer = 0f;
        }
    }

    public void SpawnFruit()
    {
        if (!_hasFruit)
        {
            TreeStretch_Anim();
            _hasFruit = true;
            _treeSprite.sprite = _sprite[1];
        }
    }


    private void TreeStretch_Anim()
    {
        _treeAnim.CrossFade(TreeStretch, 0f);
    }
    private static readonly int TreeStretch = Animator.StringToHash("Tree-Stretch");

}

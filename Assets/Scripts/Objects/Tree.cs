
using UnityEngine;
public class Tree : MonoBehaviour
{
    // TODO : tach rieng effect
    [Tooltip("0: No fruit, 1: Has fruit")]
    [SerializeField] protected Sprite[] _sprite;
    [SerializeField] protected SpriteRenderer _treeSprite;
    [SerializeField] protected ItemType _fruitType;

    IEffect _effect;

    protected bool _hasBeenClicked = false;
    protected bool _hasFruit = true;
    protected float _timeToSpawn = 8f;
    protected float _timeToSpawnTimer = 0f;
    protected Player _player;
    protected ResourceManager _rM;
    protected void Start()
    {
        _player = Player.Instance;
        _rM = ResourceManager.Instance;
        _effect = GetComponent<IEffect>();
        _hasFruit = true;
        _treeSprite.sprite = _sprite[1];
    }
    protected void Update()
    {
        // Change order layer
        float rangeradius = 1.5f;

        ChangeOrderLayer();
        if (_hasBeenClicked)
        {
            // if player click in other position then tree will not take fruit
            if (Input.GetMouseButtonDown(0))
            {
                _hasBeenClicked = false;
            }
            else if (_player.playerState == PlayerState.Idle && DistanceToPlayer() <= rangeradius && _hasFruit)
            {
                _hasBeenClicked = false;
                TakeFruit();
                // Update number of fruit 
                UpdateFruit();
            }
        }

        // Timer to spawn fruit
        _timeToSpawnTimer = _hasFruit ? 0f : _timeToSpawnTimer + Time.deltaTime;
        if (_timeToSpawnTimer >= _timeToSpawn)
        {
            SpawnFruit();
        }
    }

    public void ChangeOrderLayer()
    {
        float cheatDistance = 0.5f;
        if (_player.transform.position.y + cheatDistance > transform.position.y)
        {
            _treeSprite.sortingOrder = 10;
        }
        else
        {
            _treeSprite.sortingOrder = 3;
        }
    }

    public void SpawnFruit()
    {
        if (!_hasFruit)
        {
            _hasFruit = true;
            _treeSprite.sprite = _sprite[1];

            _effect.Effect(); // spawn fruit effect
        }
    }


    public float DistanceToPlayer()
    {
        float distance = Vector2.Distance(_player.transform.position, transform.position);
        return distance;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
    public void OnMouseUpAsButton()
    {
        _hasBeenClicked = true;
    }

    public void TakeFruit()
    {
        if (_hasFruit)
        {
            _hasFruit = false;
            _treeSprite.sprite = _sprite[0];
            _timeToSpawnTimer = 0f;

            _effect.Effect(); // take fruit effect
        }
    }

    protected void UpdateFruit()
    {
        // plus 1 fruit
        int newAmout = _rM.GetItem(_fruitType).amount += 1;
        _rM.SetAmoutItem(_fruitType, newAmout);
    }
}

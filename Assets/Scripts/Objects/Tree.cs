
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
        Debug.Log("Tree Start called");
    }
    protected void Update()
    {
        if (_hasBeenClicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _hasBeenClicked = false;
            }
            else if (!_player.CheckMoving() && DistanceToPlayer() <= 1.5f && _hasFruit)
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

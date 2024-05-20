
using UnityEngine;
using UnityEngine.UIElements;
public class Tree : Clicker
{
    private bool _hasFruit = true;
    private float _timeToSpawn = 8f;
    private float _timeToSpawnTimer = 0f;
    private bool _playerInRange = false;
    private IEffect _effect;
    private ResourceManager _rM;
    private Player _player;

    [Tooltip("0: No fruit, 1: Has fruit")]
    [SerializeField] protected Sprite[] _sprite;
    [SerializeField] protected SpriteRenderer _treeSprite;
    [SerializeField] protected ItemType _fruitType;

    private void Start()
    {
        _rM = ResourceManager.Instance;
        _player = Player.Instance;
        _effect = GetComponent<IEffect>();
        _hasFruit = true;
        _treeSprite.sprite = _sprite[1];
    }
    protected override void Update()
    {
        base.Update();
        _timeToSpawnTimer = _hasFruit ? 0f : _timeToSpawnTimer + Time.deltaTime;
        if (_timeToSpawnTimer >= _timeToSpawn)
        {
            SpawnFruit();
        }
    }



    protected override void HandleClick()
    {
        if (_player.playerState == PlayerState.Idle && _playerInRange && _hasFruit)
        {
            _hasBeenClicked = false;
            TakeFruit();
            // Update number of fruit 
            UpdateFruit();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D : " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            _playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }


}

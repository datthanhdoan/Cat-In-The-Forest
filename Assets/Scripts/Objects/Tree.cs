
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;
public class Tree : Clicker, IInteractive
{
    private bool _hasFruit = true;
    private float _timeToSpawnTimer = 0f;
    private bool _playerInRange = false;
    private int _stateOfTree;
    private int _currentState = 1;
    private IEffect _effect;
    private ResourceManager _rM;
    private Player _player;
    [SerializeField] private float _timeToSpawn = 8f;

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

        _stateOfTree = _sprite.Length;

        UpdateSprite(_sprite.Length - 1);

    }
    protected override void Update()
    {
        base.Update();
        _timeToSpawnTimer = _hasFruit ? 0f : _timeToSpawnTimer + Time.deltaTime;
        // if tree has more than 2 states 
        if (_stateOfTree > 2 && !_hasFruit)
        {
            // eg if tree has 3 states , fraction of time to spawn fruit is 1/3 and so on
            var timeFraction = _timeToSpawn * (_currentState / (float)_sprite.Length);
            if (_timeToSpawnTimer >= timeFraction)
            {
                _currentState++;
                if (_currentState > _sprite.Length)
                {
                    _currentState = 1;
                }
                UpdateSprite(_currentState - 1);
            }
        }

        if (_timeToSpawnTimer >= _timeToSpawn)
        {
            SpawnFruit();
        }
    }

    private void UpdateSprite(int index)
    {
        _treeSprite.sprite = _sprite[index];
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

    protected void HandelGamepadClick()
    {
        if (_playerInRange && _hasFruit)
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
            _treeSprite.sprite = _sprite[_sprite.Length - 1];

            _effect.Effect(); // spawn fruit effect
        }
    }

    public void TakeFruit()
    {
        if (_hasFruit)
        {
            _hasFruit = false;
            UpdateSprite(0);
            _timeToSpawnTimer = 0f;

            _effect.Effect(); // take fruit effect
        }
    }

    private void UpdateFruit()
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
            _player.SetInteractiveObject(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != null && other.gameObject != null)
        {
            Debug.Log("OnTriggerExit2D : " + other.gameObject.name);
            if (other.CompareTag("Player"))
            {
                _playerInRange = false;
                if (other.TryGetComponent(out Player player))
                {
                    var interactiveObject = player.GetInteractiveObject();
                    if (interactiveObject != null && interactiveObject.Equals(this))
                    {
                        player.SetInteractiveObject(null);
                    }
                }
            }
        }
    }

    public void OnInteractive()
    {
        HandelGamepadClick();
    }

    public void OnButtonInteractive() { }
}

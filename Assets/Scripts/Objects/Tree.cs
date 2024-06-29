using UnityEngine;

public class Tree : Clicker, IInteractive
{
    private bool _hasFruit = true;
    private float _timeToSpawnTimer = 0f;
    private bool _playerInRange = false;
    private int _stateOfTree;
    private int _currentState = 0; // Khởi tạo là 0 để phù hợp với mảng sprite
    private IEffect _effect;
    private ResourceManager _resourceManager;
    private Player _player;
    [SerializeField] private float _timeToSpawn = 8f;

    [Tooltip("0: No fruit, 1: Has fruit")]
    [SerializeField] protected Sprite[] _sprite;
    [SerializeField] protected SpriteRenderer _treeSprite;
    [SerializeField] protected ItemType _fruitType;

    #region Init
    private void Start()
    {
        InitializeComponents();
        InitializeState();
        UpdateSprite(_sprite.Length - 1);
    }

    private void InitializeComponents()
    {
        _resourceManager = ResourceManager.Instance;
        _player = Player.Instance;
        _effect = GetComponent<IEffect>();
    }

    private void InitializeState()
    {
        _hasFruit = true;
        _stateOfTree = _sprite.Length;
    }

    #endregion

    protected override void Update()
    {
        base.Update();
        _timeToSpawnTimer = _hasFruit ? 0f : _timeToSpawnTimer + Time.deltaTime;
        HandelUpdateSprite();
        if (_timeToSpawnTimer >= _timeToSpawn)
        {
            SpawnFruit();
        }
    }

    private void HandelUpdateSprite()
    {
        if (_stateOfTree > 2 && !_hasFruit)
        {
            // Tính toán thời gian cho mỗi trạng thái
            var timeFraction = _timeToSpawn / (_sprite.Length - 1);
            if (_timeToSpawnTimer >= timeFraction * (_currentState + 1))
            {
                UpdateTreeState();
            }
        }
    }

    private void UpdateTreeState()
    {
        _currentState++;
        if (_currentState >= _sprite.Length - 1)
        {
            _currentState = 0;
        }
        UpdateSprite(_currentState);
    }

    private void UpdateSprite(int index)
    {
        _treeSprite.sprite = _sprite[index];
    }

    protected override void HandleClick()
    {
        if (_playerInRange && _hasFruit)
        {
            ProcessClick();
        }
    }

    protected void HandelGamepadClick()
    {
        if (_playerInRange && _hasFruit)
        {
            ProcessClick();
        }
    }

    private void ProcessClick()
    {
        _hasBeenClicked = false;
        TakeFruit();
        UpdateFruit();
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
        int newAmout = _resourceManager.GetItem(_fruitType).amount += 1;
        _resourceManager.SetAmoutItem(_fruitType, newAmout);
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
        if (!IsValidCollider(other)) return;

        Debug.Log("OnTriggerExit2D : " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            HandlePlayerExit(other);
        }
    }

    private bool IsValidCollider(Collider2D other)
    {
        return other != null && other.gameObject != null;
    }

    private void HandlePlayerExit(Collider2D other)
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


    public void OnInteractive()
    {
        HandelGamepadClick();
    }

    public void OnButtonInteractive() { }
}

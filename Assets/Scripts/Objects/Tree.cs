
using UnityEngine;
using UnityEngine.UIElements;
public class Tree : ClickableWithLayerChange
{
    // TODO : tach rieng effect

    [Tooltip("0: No fruit, 1: Has fruit")]
    [SerializeField] protected Sprite[] _sprite;
    [SerializeField] protected SpriteRenderer _treeSprite;
    [SerializeField] protected ItemType _fruitType;

    private IEffect _effect;

    private bool _hasFruit = true;
    private float _timeToSpawn = 8f;
    private float _timeToSpawnTimer = 0f;
    private ResourceManager _rM;

    protected override void Start()
    {
        base.Start();
        _rM = ResourceManager.Instance;
        _effect = GetComponent<IEffect>();
        _hasFruit = true;
        _treeSprite.sprite = _sprite[1];
    }
    protected override void Update()
    {
        base.Update();
        // Change order layer
        ChangeOrderLayer(_treeSprite, _useOrderLayer, _cheatDistance, _orderToFront, _orderToBack);

        // Timer to spawn fruit
        _timeToSpawnTimer = _hasFruit ? 0f : _timeToSpawnTimer + Time.deltaTime;
        if (_timeToSpawnTimer >= _timeToSpawn)
        {
            SpawnFruit();
        }
    }



    protected override void HandleClick()
    {
        float rangeradius = 1.5f;
        if (_player.playerState == PlayerState.Idle && DistanceToPlayer() <= rangeradius && _hasFruit)
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


    public float DistanceToPlayer()
    {
        float distance = Vector2.Distance(_player.transform.position, transform.position);
        return distance;
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
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }

}

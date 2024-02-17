
using UnityEngine;
public class Tree : MonoBehaviour
{
    [Tooltip("0: No fruit, 1: Has fruit")]
    [SerializeField] protected Sprite[] _sprite;
    [SerializeField] protected SpriteRenderer _treeSprite;
    [Tooltip("Tree stretch animation from treeSprite object")]
    [SerializeField] protected Animator _treeAnim;

    protected bool _hasBeenClicked = false;
    protected bool _hasFruit = false;
    protected float _timeToSpawn = 8f;
    protected float _timeToSpawnTimer = 0f;
    protected int _fruitValue = 1;
    protected ProgressManager _pm;
    protected GameManagerment _gm;
    protected Player _player;
    protected void Start()
    {
        _pm = ProgressManager.instance;
        _gm = GameManagerment.instance;
        _player = Player.instance;
        _hasFruit = true;
        _treeSprite.sprite = _sprite[1];
    }
    protected void Update()
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

                // Update number of fruit 
                // _pm.UpdateApple(1);
                UpdateFruit(1);

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

    public void SpawnFruit()
    {
        if (!_hasFruit)
        {
            TreeStretch_Anim();
            _hasFruit = true;
            _treeSprite.sprite = _sprite[1];
        }
    }

    protected virtual void UpdateFruit(int value)
    {
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



    private void TreeStretch_Anim()
    {
        _treeAnim.CrossFade(TreeStretch, 0f);
    }
    private static readonly int TreeStretch = Animator.StringToHash("Tree-Stretch");

}

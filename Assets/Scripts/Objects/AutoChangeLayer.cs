using UnityEngine;

public class AutoChangeLayer : MonoBehaviour
{
    private int _orderToFront = 10;
    private int _orderToBack = 3;
    [SerializeField] private float _cheatDistance = 0.5f;
    [SerializeField] private bool _useOrderLayer = true;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Player _player;
    private void Start()
    {
        _player = Player.Instance;
    }
    private void FixedUpdate()
    {
        if (_player.playerState == PlayerState.Move)
        {
            ChangeOrderLayer(_spriteRenderer, _useOrderLayer, _cheatDistance, _orderToFront, _orderToBack);
        }
    }
    public void ChangeOrderLayer(SpriteRenderer spriteRenderer, bool useOrderLayer, float cheatDistance, int orderToFront, int orderToBack)
    {
        if (!useOrderLayer) return;
        if (_player.transform.position.y + cheatDistance > transform.position.y)
        {
            spriteRenderer.sortingOrder = orderToFront;
        }
        else
        {
            spriteRenderer.sortingOrder = orderToBack;
        }
    }
}
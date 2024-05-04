using UnityEngine;

public class ClickableWithLayerChange : Clicker
{
    protected int _orderToFront = 10;
    protected int _orderToBack = 3;
    protected float _cheatDistance = 0.5f;
    [SerializeField] protected bool _useOrderLayer = true;
    protected Player _player;
    protected virtual void Start()
    {
        _player = Player.Instance;
    }
    public virtual void ChangeOrderLayer(SpriteRenderer spriteRenderer, bool useOrderLayer, float cheatDistance, int orderToFront, int orderToBack)
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
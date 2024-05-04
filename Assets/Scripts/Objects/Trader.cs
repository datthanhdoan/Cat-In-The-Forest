using UnityEngine;

public class Trader : ClickableWithLayerChange
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    protected override void Start()
    {
        base.Start();
        _orderToBack = 4;
    }
    protected override void Update()
    {
        base.Update();
        ChangeOrderLayer(_spriteRenderer, _useOrderLayer, _cheatDistance, _orderToFront, _orderToBack);
    }
}
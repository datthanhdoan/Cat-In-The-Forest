using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;
using TMPro;
using DG.Tweening;

public class ItemPopup : MonoBehaviour, IItem
{
    public ItemType _itemType { get; private set; }
    public int _amount { get; private set; }
    private ResourceManager _resourceManager;
    private ObjectPool<ItemPopup> _pool;
    [SerializeField] private TextMeshProUGUI _amountText;

    [SerializeField] private Image _itemImage;
    private Transform _transformParent;

    public void SetItem(ItemType itemType, int amount)
    {
        if (_resourceManager == null)
            _resourceManager = ResourceManager.Instance;

        this._itemType = itemType;
        this._amount = amount;

        _itemImage.sprite = _resourceManager.GetItemSprite(_itemType);
        int amoutVisual = _amount - _resourceManager.GetAmountOfItem(_itemType);
        _amountText.text = amoutVisual > 1 ? amoutVisual.ToString() : "";
    }

    private void OnDisable()
    {
        _itemType = ItemType.Wood;
        _amount = 0;
        _itemImage.sprite = null;
    }

    public void OnClick()
    {
        if (_resourceManager == null)
            _resourceManager = ResourceManager.Instance;
        _resourceManager.SetAmoutItem(_itemType, _amount);
        GetComponent<IShowHide>().Hide(_transformParent).OnComplete(() =>
        {
            _pool.Release(this);
        });
    }

    public void SetTransformParent(Transform transformParent)
    {
        this._transformParent = transformParent;
        ShowGO();
    }

    public void ShowGO()
    {
        GetComponent<IShowHide>().Show(this._transformParent);
        transform.position = this._transformParent.position;
    }
    public void SetPool(ObjectPool<ItemPopup> pool)
    {
        _pool = pool;
    }

}
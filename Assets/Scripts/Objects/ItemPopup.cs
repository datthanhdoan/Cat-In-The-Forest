using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;
using TMPro;
using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ItemPopup : MonoBehaviour, IItem
{
    public int _amount { get; private set; }
    private float duration = 1;
    private float distance = 1;
    private Vector3 _originalPosition;
    private List<IObserver> _observers = new List<IObserver>();
    public ItemType _itemType { get; private set; }
    private ResourceManager _resourceManager;
    private Transform _transformParent;
    [SerializeField] private TextMeshProUGUI _amountText;

    [SerializeField] private Image _itemImage;

    public void AddObserver(IObserver observer)
    {
        _observers.Add(observer);
    }
    public void SetItem(ItemType itemType, int amount)
    {
        if (_resourceManager == null)
            _resourceManager = ResourceManager.Instance;

        this._itemType = itemType;
        this._amount = amount;

        _itemImage.sprite = _resourceManager.GetItemSprite(_itemType);
        int amoutVisual = _amount;
        _amountText.text = amoutVisual > 1 ? amoutVisual.ToString() : "";
    }

    private void OnDisable()
    {
        _itemType = ItemType.Wood;
        _amount = 0;
        _itemImage.sprite = null;
        _observers.Clear();
    }

    public void OnClick()
    {
        if (_resourceManager == null)
            _resourceManager = ResourceManager.Instance;
        var currentItemAmount = _resourceManager.GetAmountOfItem(_itemType);
        _resourceManager.SetAmoutItem(_itemType, currentItemAmount + this._amount);
        // notify observers that the item has been clicked
        if (_observers.Count > 0)
        {
            foreach (var observer in _observers)
            {
                observer.OnNotify();
            }
        }
        GetComponent<IShowHide>().Hide(this.transform, distance, duration).OnComplete(
            () => { gameObject.SetActive(false); });
    }

    public void SetTransformParentAndShow(Transform transformParent)
    {
        this._transformParent = transformParent;
        ShowGO();
    }

    public void ShowGO()
    {

        GetComponent<IShowHide>().Show(this.transform, distance, duration);
        transform.position = this._transformParent.position;
    }


}
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Trader : Clicker
{
    private int _maxItem;
    private int _diamondAward;
    private ResourceManager _rM;
    /// Dictionary of item in table
    private Dictionary<int, ItemType> _itemsDic = new Dictionary<int, ItemType>(); // index of item - item type
    private ItemType[] _itemTypes = new ItemType[3] {
        ItemType.AppleCandy,
        ItemType.GrapeCandy,
        ItemType.Honey
    };
    private int[] _requiredItemAmounts = new int[3];
    /// List of item in table
    private List<GameObject> items = new List<GameObject>();
    [SerializeField] private GameObject itemParent;

    /// Award item
    [SerializeField] private TextMeshProUGUI awardText;
    private void Start()
    {
        for (int i = 0; i < itemParent.transform.childCount; i++)
        {
            items.Add(itemParent.transform.GetChild(i).gameObject);
        }
        _maxItem = _itemTypes.Length;
        _rM = ResourceManager.Instance;
        for (int i = 0; i < _maxItem; i++)
        {
            _itemsDic.Add(i, _itemTypes[i]);
        }
        UpdateItemRequire();
    }

    public void UpdateItemRequire()
    {
        // random item require
        for (int i = 0; i < _maxItem; i++)
        {
            _requiredItemAmounts[i] = Random.Range(0, 10);
        }
        // ramdom coin award
        _diamondAward = Random.Range(0, 5);

        // cập nhat số lượng item vào table
        for (int i = 0; i < items.Count; i++)
        {
            var image = items[i].transform.GetChild(0).GetComponent<Image>();
            var text = items[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();

            image.sprite = _rM.GetItemSprite(_itemsDic[i]);
            text.text = _requiredItemAmounts[i].ToString();
        }
        // Cập nhật diamond award
        awardText.text = _diamondAward.ToString();

    }

    public void CheckAward()
    {
        bool isEnough = true;
        // loop through all item in list
        for (int i = 0; i < _maxItem; i++)
        {
            if (_rM.GetAmountOfItem(_itemsDic[i]) < _requiredItemAmounts[i])
            {
                isEnough = false;
                break;
            }
        }

        if (isEnough)
        {
            _rM.SetDiamond(_rM.GetDiamond() + _diamondAward);
            UpdateItemRequire();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ResourceManager : GenericSingleton<ResourceManager>
{
    // Quan ly resource
    public static event Action OnResourceChange;
    [SerializeField] private ItemSO _itemSO;
    private int _coin = 0;
    private int _diamond = 0;

    #region Setters
    public void SetDiamond(int diamond)
    {
        _diamond = diamond;
        OnResourceChange?.Invoke();
    }

    public void SetCoin(int coin)
    {
        _coin = coin;
        OnResourceChange?.Invoke();
    }

    public void SetAmoutItem(ItemType itemType, int amount)
    {
        foreach (GameObject item in _itemSO.itemList)
        {
            if (item.GetComponent<Item>().type == itemType)
            {
                item.GetComponent<Item>().amount = amount;
            }
        }
        OnResourceChange?.Invoke();
    }
    public void SetItemDataList(List<ItemData> itemDataList)
    {
        for (int i = 0; i < itemDataList.Count; i++)
        {
            foreach (GameObject item in _itemSO.itemList)
            {
                if (item.GetComponent<Item>().type.ToString() == itemDataList[i].name)
                {
                    item.GetComponent<Item>().amount = itemDataList[i].amount;
                }
            }
        }
        OnResourceChange?.Invoke();
    }

    #endregion

    #region Getters
    public int GetCoin()
    {
        return _coin;
    }

    public int GetDiamond()
    {
        return _diamond;
    }

    public List<ItemData> GetItemDataList()
    {
        List<ItemData> itemDataList = new List<ItemData>();
        foreach (GameObject item in _itemSO.itemList)
        {
            ItemData itemData = new ItemData();
            itemData.name = item.GetComponent<Item>().type.ToString();
            itemData.amount = item.GetComponent<Item>().amount;
            Debug.Log(itemData.name + " " + itemData.amount);
            itemDataList.Add(itemData);
        }
        Debug.Log(itemDataList);
        return itemDataList;
    }

    public List<GameObject> GetGameObjectList()
    {
        return _itemSO.itemList;
    }
    public List<Item> GetItemList()
    {
        List<Item> items = new List<Item>();
        foreach (GameObject item in _itemSO.itemList)
        {
            items.Add(item.GetComponent<Item>());
        }
        return items;
    }
    public Item GetItem(ItemType itemType)
    {
        foreach (GameObject item in _itemSO.itemList)
        {
            if (item.GetComponent<Item>().type == itemType)
            {
                return item.GetComponent<Item>();
            }
        }
        return null;
    }

    public int GetAmountOfItem(ItemType itemType)
    {
        foreach (GameObject item in _itemSO.itemList)
        {
            if (item.GetComponent<Item>().type == itemType)
            {
                return item.GetComponent<Item>().amount;
            }
        }
        return 0;
    }

    public void CleaAmountOfAllItems()
    {
        foreach (GameObject item in _itemSO.itemList)
        {
            item.GetComponent<Item>().amount = 0;
        }
        OnResourceChange?.Invoke();
    }

    #endregion

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class ItemData
{
    public string name;
    public int amount;
}

public class ResourceManager : Subject
{
    // Quan ly resource
    public static ResourceManager Instance;
    [SerializeField] private ItemSO _itemSO;
    [SerializeField] private MoneySO _moneySO;



    private void Awake()
    {
        // sigleton
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
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
        NotifyObservers();
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

    public void SetAmoutItem(ItemType itemType, int amount)
    {
        foreach (GameObject item in _itemSO.itemList)
        {
            if (item.GetComponent<Item>().type == itemType)
            {
                item.GetComponent<Item>().amount = amount;
            }
        }
        NotifyObservers();
    }

    public void CleaAmountOfAllItems()
    {
        foreach (GameObject item in _itemSO.itemList)
        {
            item.GetComponent<Item>().amount = 0;
        }
        NotifyObservers();
    }

}


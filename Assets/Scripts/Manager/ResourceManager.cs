using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceManager : Subject
{
    // Quan ly resource
    private static ResourceManager instance;

    public static ResourceManager Instance
    {
        get
        {
            // if instance is null
            if (instance == null)
            {
                // find the generic instance
                instance = FindObjectOfType<ResourceManager>();

                // if it's null again create a new object
                // and attach the generic instance
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(ResourceManager).Name;
                    instance = obj.AddComponent<ResourceManager>();
                }
            }
            return instance;
        }
    }
    [SerializeField] private ItemSO _itemSO;
    [SerializeField] private MoneySO _moneySO;

    public virtual void Awake()
    {
        // create the instance
        if (instance == null)
        {
            instance = this as ResourceManager;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
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


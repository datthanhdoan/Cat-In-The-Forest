using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        NotifyObservers();
    }

    public List<Item> GetItemList()
    {
        List<Item> items = new List<Item>();
        foreach (GameObject item in _itemSO.ItemList)
        {
            items.Add(item.GetComponent<Item>());
        }
        return items;
    }
    public Item GetItem(ItemType itemType)
    {
        foreach (GameObject item in _itemSO.ItemList)
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
        foreach (GameObject item in _itemSO.ItemList)
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
        foreach (GameObject item in _itemSO.ItemList)
        {
            item.GetComponent<Item>().amount = 0;
        }
        NotifyObservers();
    }

}


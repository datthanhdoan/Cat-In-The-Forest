using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : GenericSingleton<ResourceManager>
{
    // Quan ly resource
    [SerializeField] private ItemSO _itemSO;
    [SerializeField] private MoneySO _moneySO;

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
    }

    public void CleaAmountOfAllItems()
    {
        foreach (GameObject item in _itemSO.ItemList)
        {
            item.GetComponent<Item>().amount = 0;
        }
    }

}


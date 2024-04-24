using UnityEngine;
using System;
using System.Collections.Generic;
[System.Serializable]
public class Item : MonoBehaviour
{
    public ItemType type;
    public int amount;
}

[Serializable]
public class ItemData
{
    public string name;
    public int amount;
}
public class Resource
{
    public int coin;
    public int diamond;
    public List<ItemData> itemList;
}

public enum ItemType
{
    Wood,
    Apple,
    Grape,
    AppleCandy,
    GrapeCandy,
}
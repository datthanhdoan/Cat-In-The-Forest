using UnityEngine;
using System;
using System.Collections.Generic;
[Serializable]
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

// Pricing
// Apple: 10
// Wood: 15
// Grape: 20
// AppleCandy: 30
// GrapeCandy: 40
// Honey: 25
// Rice: 8
// Tomato: 12

public enum ItemType
{
    Wood,
    Apple,
    Grape,
    AppleCandy,
    GrapeCandy,
    Honey,
    Rice,
    Tomato
}
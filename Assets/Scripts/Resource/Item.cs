using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class Item : MonoBehaviour
{
    public ItemType type;
    public int amount;
}

public class ItemDataList
{
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
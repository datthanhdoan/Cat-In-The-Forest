using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite image;
    public ItemType type;
    public int amount;
}

public enum ItemType
{
    Wood,
    Apple,
    Grape,
    AppleCandy,
    GrapeCandy,
}
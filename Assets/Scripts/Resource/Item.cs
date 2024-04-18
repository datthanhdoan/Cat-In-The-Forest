using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite sprite;
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
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Resource", menuName = "ScriptableObjects/Resource")]

public class ResourceSO : ScriptableObject
{
    public int coin { get; private set; } = 0;

    public List<Resource> resources = new List<Resource>();
    public void SetCoin(int value) => coin = value;

}
public enum ResourceName
{
    Apple,
    Grape,
    CandiedApple,
    CandiedGrape,
    Wood,
}
[System.Serializable]
public class Resource
{
    public ResourceName name;
    public Sprite sprite;
    public int quantity;
    public void SetQuantity(int value) => quantity = value;
}
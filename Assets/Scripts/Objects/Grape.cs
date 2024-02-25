using UnityEngine;

public class Grape : Tree
{
    ResourceManager.Resource grape;
    protected new void Start()
    {
        base.Start();
        _fruitValue = 1;
        _timeToSpawn = 13f;
        grape = _resourceManager.GetResource(ResourceManager.ResourceName.Grape);
    }

    protected override void UpdateFruit(int value)
    {
        grape.SetQuantity(grape.quantity + value);
        Debug.Log("Grape: " + grape.quantity);
    }
}
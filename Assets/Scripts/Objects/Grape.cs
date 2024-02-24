using UnityEngine;

public class Grape : Tree
{
    new protected int _fruitValue = 1;
    new protected float _timeToSpawn = 13f;
    ResourceManager.Resource grape;
    new protected void Start()
    {
        base.Start();
        grape = _resourceManager.GetResource(ResourceManager.ResourceName.Grape);
    }

    protected override void UpdateFruit(int value)
    {
        grape.SetQuantity(grape.quantity + value);
        Debug.Log("Grape: " + grape.quantity);
    }
}
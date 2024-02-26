using UnityEngine;

public class Grape : Tree
{
    Resource _grape;
    protected new void Start()
    {
        base.Start();
        _fruitValue = 1;
        _timeToSpawn = 13f;
        _grape = _resourceSO.resources.Find(x => x.name == ResourceName.Grape);
    }

    protected override void UpdateFruit()
    {
        _grape.SetQuantity(_grape.quantity + _fruitValue);
        Debug.Log("Grape: " + _grape.quantity);
    }
}
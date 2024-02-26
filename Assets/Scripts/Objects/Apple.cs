using UnityEngine;

public class Apple : Tree
{
    Resource _apple;
    protected new void Start()
    {
        base.Start();
        _fruitValue = 1;
        _timeToSpawn = 8f;
        _apple = _resourceSO.resources.Find(x => x.name == ResourceName.Apple);
    }
    protected override void UpdateFruit()
    {
        _apple.SetQuantity(_apple.quantity + _fruitValue);
        Debug.Log("Apple: " + _apple.quantity);
    }
}

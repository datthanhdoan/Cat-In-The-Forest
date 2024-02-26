using UnityEngine;

public class Wood : Tree
{
    Resource wood;
    protected new void Start()
    {
        base.Start();
        _fruitValue = 1;
        _timeToSpawn = 10f;
        wood = _resourceSO.resources.Find(x => x.name == ResourceName.Wood);
    }
    protected override void UpdateFruit()
    {
        wood.SetQuantity(wood.quantity + _fruitValue);
    }
}
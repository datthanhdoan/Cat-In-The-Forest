using UnityEngine;

public class Wood : Tree
{
    protected new void Start()
    {
        base.Start();
        _fruitValue = 1;
        _timeToSpawn = 10f;
        if (_resourceManager == null)
        {
            Debug.Log("Resource Manager is null");
        }
    }
    protected override void UpdateFruit(int value)
    {
        var wood = _resourceManager.GetResource(ResourceManager.ResourceName.Wood);
        wood.SetQuantity(wood.quantity + value);
    }
}
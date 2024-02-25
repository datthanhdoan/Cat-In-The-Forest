using UnityEngine;

public class Apple : Tree
{
    protected new void Start()
    {
        base.Start();
        _fruitValue = 1;
        _timeToSpawn = 8f;
        if (_resourceManager == null)
        {
            Debug.Log("Resource Manager is null");
        }
    }
    protected override void UpdateFruit(int value)
    {
        var apple = _resourceManager.GetResource(ResourceManager.ResourceName.Apple);
        apple.SetQuantity(apple.quantity + value);
    }
}

using UnityEngine;

public class Apple : Tree
{
    new protected int _fruitValue = 1;
    new protected float _timeToSpawn = 8f;
    ResourceManager.Resource apple;
    protected new void Start()
    {
        base.Start();
        apple = _resourceManager.GetResource(ResourceManager.ResourceName.Apple);
        if (_resourceManager == null)
        {
            Debug.Log("Resource Manager is null");
        }
    }
    protected override void UpdateFruit(int value)
    {
        apple.SetQuantity(apple.quantity + value);
    }
}
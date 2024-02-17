using UnityEngine;

public class Apple : Tree
{
    new protected int _fruitValue = 1;
    new protected float _timeToSpawn = 8f;
    new protected void Start()
    {
        base.Start();
    }
    protected override void UpdateFruit(int value)
    {
        _pm.UpdateApple(value);
    }
}
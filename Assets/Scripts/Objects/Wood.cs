using UnityEngine;

public class Wood : Tree
{
    new protected int _fruitValue = 1;
    new protected float _timeToSpawn = 10f;
    new protected void Start()
    {
        base.Start();
    }
    protected override void UpdateFruit(int value)
    {
        _pm.UpdateWood(value); // value is _fruitValue
    }
}
using UnityEngine;

public class Grape : Tree
{
    new protected int _fruitValue = 1;
    new protected float _timeToSpawn = 13f;
    protected override void UpdateFruit(int value)
    {
        _pm.UpdateGrape(value);
    }
}
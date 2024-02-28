using UnityEngine;

public class Grape : Tree
{
    protected override void Start()
    {
        base.Start();
        _fruitValue = 1;
        _timeToSpawn = 13f;
        _treeType = _resourceSO.resources.Find(x => x.name == ResourceName.Grape);
    }
}
using UnityEngine;

public class Apple : Tree
{
    protected override void Start()
    {
        base.Start();
        _fruitValue = 1;
        _timeToSpawn = 8f;
        _treeType = _resourceSO.resources.Find(x => x.name == ResourceName.Apple);
    }
}

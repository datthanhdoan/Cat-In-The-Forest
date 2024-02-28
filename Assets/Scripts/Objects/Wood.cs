using UnityEngine;

public class Wood : Tree
{
    protected override void Start()
    {
        base.Start();
        _fruitValue = 1;
        _timeToSpawn = 10f;
        _treeType = _resourceSO.resources.Find(x => x.name == ResourceName.Wood);
    }
}
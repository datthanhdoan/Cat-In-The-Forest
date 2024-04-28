using NavMeshPlus.Components;
using UnityEngine;

public class MapManager : GenericSingleton<MapManager>
{
    [SerializeField] NavMeshSurface Surface2D;
    public GameObject region;
    private int level = 1; // 4 is just for the testing 
    private int maxLevel = 0;

    private void Start()
    {
        Surface2D.BuildNavMeshAsync(); // Init NavMesh
        maxLevel = region.transform.childCount;
    }

    public void UpdateNavMesh()
    {
        Surface2D.UpdateNavMesh(Surface2D.navMeshData);
    }
    public int GetMaxLevel() => maxLevel;
    public void Setlevel(int level) => this.level = level;
    public int GetLevel() => level;

}
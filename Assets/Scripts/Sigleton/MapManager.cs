using NavMeshPlus.Components;
using UnityEngine;

public class MapManager : GenericSingleton<MapManager>
{
    [SerializeField] NavMeshSurface Surface2D;
    public GameObject region;
    public int level { get; private set; } = 1;
    public int maxLevel { get; private set; } = 0;

    private void Start()
    {
        Surface2D.BuildNavMeshAsync(); // Init NavMesh
        maxLevel = region.transform.childCount;
    }

    public void UpdateNavMesh()
    {
        Surface2D.UpdateNavMesh(Surface2D.navMeshData);
    }
    public bool CheckMaxLevel() => level > maxLevel;
    public void UpdateLevel() => level++;
}
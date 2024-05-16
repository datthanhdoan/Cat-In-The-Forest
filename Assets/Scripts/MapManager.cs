using NavMeshPlus.Components;
using UnityEngine;

public class MapManager : GenericSingleton<MapManager>
{
    public RegionData _regionData { get; private set; }
    public GameObject region;
    private int level = 1; // default level
    private int maxLevel = 1; // default level
    [SerializeField] NavMeshSurface Surface2D;

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
    public void Setlevel(int level)
    {
        this.level = level;
        _regionData = new RegionData
        {
            level = level
        };

        UpdateRegions(level);
    }
    public int GetLevel() => level;

    private void UpdateRegions(int index)
    {
        for (int i = 0; i < region.transform.childCount; i++)
        {
            if (i <= index - 1)
            {
                var regionChild = region.transform.GetChild(i);
                regionChild.gameObject.SetActive(true);

                // Hide connected regions , but show the last one
                // region has child and not the last region
                if (regionChild.transform.childCount > 0 && i != index - 1)
                {
                    regionChild.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            else
            {
                region.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
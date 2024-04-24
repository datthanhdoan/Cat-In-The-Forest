using NavMeshPlus.Components;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance { get; private set; }
    [SerializeField] NavMeshSurface Surface2D;
    public GameObject region;
    public int level { get; private set; } = 4; // 4 is just for the testing 
    public int maxLevel { get; private set; } = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
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
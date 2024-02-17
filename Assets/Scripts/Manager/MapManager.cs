using NavMeshPlus.Components;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance { get; private set; }
    [SerializeField] NavMeshSurface Surface2D;
    public GameObject region;
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
    }

    public void UpdateNavMesh() => Surface2D.UpdateNavMesh(Surface2D.navMeshData);
}
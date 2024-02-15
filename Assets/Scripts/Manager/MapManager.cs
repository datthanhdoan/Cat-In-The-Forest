using NavMeshPlus.Components;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance { get; private set; }
    private NavMeshSurface Surface2D;
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
        Surface2D = GameObject.FindGameObjectWithTag("NavhMesh").GetComponent<NavMeshSurface>();
        Surface2D.BuildNavMeshAsync(); // Init NavMesh
    }

    public void UpdateNavMesh() => Surface2D.UpdateNavMesh(Surface2D.navMeshData);
}
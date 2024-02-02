
using System;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class MapManager : MonoBehaviour
{
    private NavMeshSurface Surface2D;
    public GameObject region;
    private void Start()
    {
        Surface2D = GameObject.Find("NavhMesh").GetComponent<NavMeshSurface>();
        Surface2D.BuildNavMeshAsync(); // Init NavMesh
    }

    public void UpdateNavMesh() => Surface2D.UpdateNavMesh(Surface2D.navMeshData);
}